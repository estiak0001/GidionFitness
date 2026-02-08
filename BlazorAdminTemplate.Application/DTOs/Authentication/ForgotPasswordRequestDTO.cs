using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class ForgotPasswordRequestDTO
    {
        public string Email { get; set; } = string.Empty;
    }
}
