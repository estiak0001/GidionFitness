using BlazorAdminTemplate.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface ILanguageService
    {
        Task<MemberLanguageResponseDTO> GetAllLanguagesAsync();
    }
}
