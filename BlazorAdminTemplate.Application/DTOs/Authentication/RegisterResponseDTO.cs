using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class RegisterResponseDTO
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberType { get; set; } = string.Empty;
        public string MemberCreated { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
