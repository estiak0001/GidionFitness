using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Memberships
{
    public class StaffCancelMembershipDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public DateTime? CancellationDate { get; set; }
        public string CancellationReason { get; set; } = string.Empty;
        public bool CancellationOverride { get; set; }

    }
    public class StaffCancelMembershipResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public DateTime? EarliestAllowedDate { get; set; }
    }
}
