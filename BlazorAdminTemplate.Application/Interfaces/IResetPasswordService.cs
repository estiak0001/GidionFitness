using BlazorAdminTemplate.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IResetPasswordService
    {
        Task<ForgotPasswordResponseDTO> SendEmailAsync(ForgotPasswordRequestDTO request);
        Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request);
        Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordRequestDTO request);
    }
}
