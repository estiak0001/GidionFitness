using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Memberships
{
    public class StaffMemberMembershipChangeDTO
    {
        public string MemberGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
    }
    public class StaffMemberMembershipChangeResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string MemberGUID { get; set; } = string.Empty;
        public string OldPaymentGroupGUID {get; set; } = string.Empty;
        public string NewPaymentGroupGUID {get; set; } = string.Empty;
        public decimal OldAmount {get; set; }
        public decimal NewAmount { get; set; }
        public DateTime ChangeDate { get; set; }

    }


}
