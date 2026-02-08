using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Members
{
    public class StaffUpdateMemberResponseDTO
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberType { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public List<string> UpdatedFields { get; set; } = new List<string>();
    }
}
