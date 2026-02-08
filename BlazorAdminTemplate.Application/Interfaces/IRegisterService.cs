using BlazorAdminTemplate.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
        event Action<bool>? RegistrationCompleted;
        Task<RegisterResponseDTO> StaffRegisterAsync(RegisterRequestDTO request);
    }
}
