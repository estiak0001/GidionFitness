using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentCardInfoResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string StaffGuid { get; set; } = string.Empty;
        public string MemberGuid { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<PaymentCards> PaymentCards { get; set; } = new List<PaymentCards>();
    }
}
