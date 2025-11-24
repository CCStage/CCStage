using CCSentinelUI.Components;
using CCSentinelUI.Components.Authentication;
using CCSentinelUI.Components.Models;
using CCSentinelUI.Data;
using Clip.Logging;
using Clip.Logging.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PreventDuplicates = false;
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
    .AddCore(option =>
    {
        option.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
    });

builder.Services.AddScoped<AdminCredentials>(sp =>
{
    using var scope = sp.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var adminUser = db.Users
        .Join(db.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
        .Join(db.Roles, temp => temp.ur.RoleId, r => r.Id, (temp, r) => new { temp.u, r })
        .Where(result => result.r.Name == "Admin")
        .Select(result => result.u)
        .FirstOrDefault();

    if (adminUser == null)
        throw new Exception("Geen gebruiker met rol Admin gevonden.");

    return new AdminCredentials(adminUser.UserName!, adminUser.PasswordHash!);
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddClipLogging(new DefaultLogSettings()
{
    LogLevel = new Dictionary<string, LogEventLevel>()
    {
        { "Default", LogEventLevel.Warning },
        { "CCSentinelUI", LogEventLevel.Debug }
    }
});

builder.Services.AddHttpClient("ServerAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7131");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.MapPost("/api/login", (HttpContext context) =>
{
    context.Response.Cookies.Append("IsAdmin", "true", new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.AddHours(12)
    });
    return Results.Ok();
});

app.MapPost("/api/logout", (HttpContext context) =>
{
    context.Response.Cookies.Delete("IsAdmin");
    return Results.Ok();
});

app.MapGet("/api/dbtest", async (ApplicationDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        return Results.Ok(new { connected = canConnect });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
