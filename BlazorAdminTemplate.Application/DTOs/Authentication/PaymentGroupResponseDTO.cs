using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Authentication
{
    public class PaymentGroupResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<PaymentGroupCriteria> PaymentGroupCriteria { get; set; } = new List<PaymentGroupCriteria>();
        public List<PaymentGroup> PaymentGroups { get; set; } = new List<PaymentGroup>();
    }
}
