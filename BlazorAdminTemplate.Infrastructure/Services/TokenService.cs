using BlazorAdminTemplate.Application.Interfaces;
using System;
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorAdminTemplate.Infrastructure.Services
{
    public class TokenService : ITokenService
    {

        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";
        private const string RefreshTokenKey = "refreshToken";


        public TokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }




        public async Task<ClaimsPrincipal> GetClaimsPrincipalFromTokenAsync()
        {
            var token = await GetTokenAsync();
            if(string.IsNullOrEmpty(token))
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var identity = new ClaimsIdentity(jwt.Claims, "jwt");
                return new ClaimsPrincipal(identity);
            }
            catch 
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(TokenKey);
        }

        public async Task<DateTime?> GetTokenExpirationAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token)) 
            {
                return null;
            }

            try 
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                return jwt.ValidTo;
            }
            catch 
            {
                return null;
            }
                
        }

        public async Task<bool> IsTokenValidAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var isValid = jwt.ValidTo > DateTime.UtcNow;

                return isValid;
            }
            catch (Exception)
            {
                // If the token is invalid, return false
                return false;
            }
        }

        public async Task RemoveTokenAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
        }

        public async Task StoreTokenAsync(string token)
        {
            await _localStorage.SetItemAsync(TokenKey, token);
        }

        public async Task StoreRefreshToken(string refreshToken)
        {
            Console.WriteLine($"Storing refresh token (Tokenservice: StoreRefreshToken): {refreshToken}");
            await _localStorage.SetItemAsync(RefreshTokenKey, refreshToken);
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(RefreshTokenKey);
        }

        public async Task RemoveRefreshTokenAsync()
        {
            await _localStorage.RemoveItemAsync(RefreshTokenKey);
        }

        public async Task<bool> IsTokenExpiringSoonAsync(int minutesBeforeExpiry = 5)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var expirationTime = jwt.ValidTo;
                var currentTime = DateTime.UtcNow;
                var timeRemaining = expirationTime - currentTime;

                var isExpiringSoon = timeRemaining.TotalMinutes <= minutesBeforeExpiry;



                return isExpiringSoon;
            }
            catch (Exception)
            {
                // If the token is invalid or cannot be parsed, consider it expiring soon
                return true;
            }
        }

        
        public async Task<string> GetTokenDebugInfoAsync()
        {
            var token = await GetTokenAsync();
            var refreshToken = await GetRefreshTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                return "No access token found";
            }

            try
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var now = DateTime.UtcNow;
                var expiration = jwt.ValidTo;
                var timeRemaining = expiration - now;

                var info = $"""
            TOKEN DEBUG INFO:
            - Current Time (UTC): {now:yyyy-MM-dd HH:mm:ss}
            - Token Expires (UTC): {expiration:yyyy-MM-dd HH:mm:ss}
            - Time Remaining: {timeRemaining.TotalMinutes:F2} minutes
            - Is Valid: {expiration > now}
            - Is Expiring Soon (5min): {await IsTokenExpiringSoonAsync(5)}
            - Has Refresh Token: {!string.IsNullOrEmpty(refreshToken)}
            """;

                return info;
            }
            catch (Exception ex)
            {
                return $"Error parsing token: {ex.Message}";
            }
        }

       
    }
}
