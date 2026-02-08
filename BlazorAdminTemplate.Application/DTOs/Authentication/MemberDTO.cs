using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class MemberDTO
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberFirstName { get; set; } = string.Empty;
        public string MemberLastName { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberPhone { get; set; } = string.Empty;
        public string MemberType { get; set; } = string.Empty;
        public DateTime MemberCreatedAt { get; set; }

        public string FullName => $"{MemberFirstName} {MemberLastName}".Trim();
    }
}
