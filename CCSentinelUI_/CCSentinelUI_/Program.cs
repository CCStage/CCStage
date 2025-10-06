using Blazored.LocalStorage;
using CCSentinelUI_.Components;
using CCSentinelUI_.Components.Authentication;
using CCSentinelUI_.Components.Models;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var adminUser = Environment.GetEnvironmentVariable("SENTINEL_ADMIN_USER", EnvironmentVariableTarget.Machine);
var adminPass = Environment.GetEnvironmentVariable("SENTINEL_ADMIN_PASS", EnvironmentVariableTarget.Machine);

if (string.IsNullOrWhiteSpace(adminUser) || string.IsNullOrWhiteSpace(adminPass))
{
    throw new Exception("SENTINEL_ADMIN_USER en SENTINEL_ADMIN_PASS moeten gezet zijn.");
}

builder.Services.AddSingleton(new AdminCredentials(adminUser, adminPass));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
