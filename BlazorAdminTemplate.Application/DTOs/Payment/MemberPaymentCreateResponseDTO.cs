using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentCreateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string PaymentIdentifier { get; set; } = string.Empty;
        public string PaymentWindowLink { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}
