using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentDTO
    {
        public string MemberPaymentDataGUID { get; set; } = string.Empty;
        public int MemberPaymentDataType { get; set; }
        public int MemberPaymentDataDirection { get; set; }
        public double MemberPaymentDataAmount { get; set; }
        public string MemberPaymentDataOrderNumber { get; set; } = string.Empty;
        public int MemberPaymentDataStatus { get; set; }
        public string MemberPaymentDataDate { get; set; } = string.Empty;
    }
}
