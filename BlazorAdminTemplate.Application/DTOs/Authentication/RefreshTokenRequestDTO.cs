using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
