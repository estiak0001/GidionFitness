using BlazorAdminTemplate.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
        Task<bool> LogoutAsync();
        Task<MemberDTO?> GetCurrentMemberAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<RefreshTokenResponseDTO?> RefreshTokenAsync();
        event Action<bool>? AuthenticationStateChanged;
    }
}

