using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Memberships
{
    public class StaffHoldMemberSubscriptionDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public DateTime HoldStartDate { get; set; }
        public DateTime HoldEndDate { get; set; }
        public string HoldComment { get; set; } = string.Empty;
    }

    public class StaffHoldMemberSubscriptionResponseDTO
    {
        public bool? Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string MemberGuid { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public HoldPeriod HoldPeriod { get; set; } = new HoldPeriod();
        public HoldCost Cost { get; set; } = new HoldCost();
        public bool EmailSent { get; set; }
        public int PaymentRecordsCreated { get; set; }
    }

    public class HoldPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays { get; set; }
    }

    public class HoldCost
    {
        public decimal TotalAmount { get; set; }
        public List<string> Breakdown { get; set; } = new List<string>();
        public string Type { get; set; } = string.Empty;
        public string FreePeriod { get; set; } = string.Empty;
    }
}
