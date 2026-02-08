using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface ITokenService
    {
        Task StoreTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task RemoveTokenAsync();
        Task<bool> IsTokenValidAsync();
        Task<ClaimsPrincipal> GetClaimsPrincipalFromTokenAsync();
        Task<DateTime?> GetTokenExpirationAsync();
        Task StoreRefreshToken(string refreshToken);
        Task<string?> GetRefreshTokenAsync();
        Task RemoveRefreshTokenAsync();
        Task<bool> IsTokenExpiringSoonAsync(int minutesBeforeExpiry = 5);
        Task<string> GetTokenDebugInfoAsync();
        
    }
}
