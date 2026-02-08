using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class PaymentDummy
    {
        public string TransactionId { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = "Debit";
        public string Status { get; set; } = "Completed";
        public string CardLast4 { get; set; } = string.Empty;
        public string CardBrand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
