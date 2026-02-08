using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class PaymentInfo
    {
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
        public double MemberPaymentPrice { get; set; }
        public double? MemberPaymentPausePrice { get; set; }
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
    }
}
