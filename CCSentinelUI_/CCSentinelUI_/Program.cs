using CCSentinelUI_.Components;
using CCSentinelUI_.Components.Authentication;
using CCSentinelUI_.Components.Models;
using CCSentinelUI_.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var adminUser = Environment.GetEnvironmentVariable("SENTINEL_ADMIN_USER", EnvironmentVariableTarget.Machine);
var adminPass = Environment.GetEnvironmentVariable("SENTINEL_ADMIN_PASS", EnvironmentVariableTarget.Machine);

if (string.IsNullOrWhiteSpace(adminUser) || string.IsNullOrWhiteSpace(adminPass))
{
    throw new Exception("SENTINEL_ADMIN_USER en SENTINEL_ADMIN_PASS moeten gezet zijn.");
}

builder.Services.AddSingleton(new AdminCredentials(adminUser, adminPass));
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddHttpContextAccessor();

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
