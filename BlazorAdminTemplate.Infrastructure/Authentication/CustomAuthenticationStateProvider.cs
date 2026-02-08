using BlazorAdminTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Infrastructure.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public CustomAuthenticationStateProvider(ILoginService loginService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _loginService = loginService;

            _loginService.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("CustomAuthenticationStateProvider: GetAuthenticationStateAsync called");
            
            var token = await _tokenService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("CustomAuthenticationStateProvider: No token found, returning anonymous");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Only refresh if token is actually INVALID (expired), not just "expiring soon"
            if (!await _tokenService.IsTokenValidAsync())
            {
                Console.WriteLine("CustomAuthenticationStateProvider: Token is expired, attempting refresh");
                
                if (!string.IsNullOrEmpty(await _tokenService.GetRefreshTokenAsync()))
                {
                    var refreshResponse = await _loginService.RefreshTokenAsync();
                    if (refreshResponse != null)
                    {
                        Console.WriteLine("CustomAuthenticationStateProvider: Token refreshed successfully");
                        var claimsPrincipal = await _tokenService.GetClaimsPrincipalFromTokenAsync();
                        return new AuthenticationState(claimsPrincipal);
                    }
                }
                
                Console.WriteLine("CustomAuthenticationStateProvider: Token expired and refresh failed, returning anonymous");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Token is still valid, use it
            Console.WriteLine("CustomAuthenticationStateProvider: Token is still valid, using existing token");
            var claims = await _tokenService.GetClaimsPrincipalFromTokenAsync();
            return new AuthenticationState(claims);
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = GetClaimsPrincipalFromToken(token);
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            try
            {
                var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwt.Claims, "jwt");
                return new ClaimsPrincipal(identity);
            }
            catch
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }
        }

        private void OnAuthenticationStateChanged(bool isAuthenticated)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Dispose()
        {
            _loginService.AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
