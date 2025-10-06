using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CCSentinelUI_.Components.Authentication
{
    public class CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return Task.FromResult(new AuthenticationState(_anonymous));

            var isAdminCookie = context.Request.Cookies["IsAdmin"];
            if (isAdminCookie == "true")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Admin")
                }, "CustomAuth");

                var user = new ClaimsPrincipal(identity);
                return Task.FromResult(new AuthenticationState(user));
            }

            return Task.FromResult(new AuthenticationState(_anonymous));
        }

        public Task MarkUserAsAuthenticated()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.CompletedTask;
        }

        public Task MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.CompletedTask;
        }
    }
}
