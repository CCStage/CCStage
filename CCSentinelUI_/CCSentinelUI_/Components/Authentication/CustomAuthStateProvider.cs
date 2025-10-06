using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CCSentinelUI_.Components.Authentication
{
    public class CustomAuthStateProvider(ILocalStorageService storage) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var isAdmin = await storage.GetItemAsync<bool>("IsAdmin");
            if (isAdmin)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Admin")
                }, "CustomAuth");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            return new AuthenticationState(_anonymous);
        }

        public async Task MarkUserAsAuthenticated()
        {
            await storage.SetItemAsync("IsAdmin", true);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await storage.RemoveItemAsync("IsAdmin");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
