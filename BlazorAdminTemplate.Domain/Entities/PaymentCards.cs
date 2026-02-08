using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class PaymentCards
    {
        public string MemberCardGUID { get; set; } = string.Empty;
        public string MemberCardCreated { get; set; } = string.Empty;
        public int MemberCardStatus { get; set; } 
        public string MemberCardMaskedPan { get; set; } = string.Empty;
        public string MemberCardCardExpiryDate { get; set; } = string.Empty;
        public int? MemberCardCardType { get; set; }

    }
}
