using BlazorAdminTemplate.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileResponseDTO?> GetProfileAsync();
        Task<bool> UpdateProfileAsync(UpdateProfileRequestDTO profile);

        Task<string?> GetProfileImageUrlAsync();
        Task<bool> DeleteProfileImageAsync();

    }
}
