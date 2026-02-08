using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Members
{
    public class StaffChangeMemberPasswordDto
    {
        public string MemberGuid { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    public class StaffChangeMemberPasswordResponseDto
    {
        //public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
