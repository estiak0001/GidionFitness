using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentMembershipInfoResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public PaymentInfo PaymentInfo { get; set; } = new PaymentInfo();
    }
}
