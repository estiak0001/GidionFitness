using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Memberships
{
    public class StaffCancelMembershipCheckDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
    }
    public class StaffCancelMembershipCheckResponseDTO
    {
        public bool CanCancel { get; set; }
        public DateTime? EarliestCancellationDate { get; set; }
        public bool CanCancelNow { get; set; }
        public DateTime MemberCreatedDate { get; set; }
        public CancellationSettings Settings { get; set; } = new CancellationSettings();
        public string Note { get; set; } = string.Empty;
    }

    public class CancellationSettings
    {
        public int BindingPeriodMonths { get; set; }
        public bool CancellationAllowedInBindingPeriod { get; set; }
        public int NoticePeriodMonths { get; set; }
    }
}
