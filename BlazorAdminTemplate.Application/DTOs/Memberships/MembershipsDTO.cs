using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Memberships
{
    public class MembershipsDTO
    {
        public string MemberPaymentGroupGUID { get; set; } = string.Empty;
        public string MemberPaymentGroupName { get; set; } = string.Empty;
        public decimal MemberPaymentPrice { get; set; }
        public string MemberPaymentGroupDescription { get; set; } = string.Empty;
        public int MemberPaymentDefault {  get; set; }

        public DateTime MemberPaymentValidFrom { get; set; }
        public DateTime MemberPaymentValidTo { get; set; }
    }

    public class MembershipsResponseDTO
    {
        public string Message { get; set; } = String.Empty;
        public int Count { get; set; }
        public List<MembershipsDTO> PaymentGroups { get; set; } = new List<MembershipsDTO>();
    }
}
