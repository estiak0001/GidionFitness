using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentInfoResponseDTO
    {
        public string PriceName { get; set; } = string.Empty;
        public double PriceMonth { get; set; }
        public double PriceDay { get; set; }
        public double PriceCurrent { get; set; }
        public string CurrentDay { get; set; } = string.Empty;
        public int DaysInCurrentMonth { get; set; }
        public int RemainingDaysInMonth { get; set; }
    }
}
