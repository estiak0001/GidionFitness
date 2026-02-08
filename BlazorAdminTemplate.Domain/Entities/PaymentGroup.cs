using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class PaymentGroup
    {
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public decimal MemberPaymentPrice { get; set; } 
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
    }
}
