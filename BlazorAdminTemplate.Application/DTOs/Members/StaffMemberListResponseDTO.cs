using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Members
{
    public class StaffMemberListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public string SearchMembership { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();

        public int Count { get; set; } = 0;
        public List<PaymentGroup> AvailablePaymentGroups { get; set; } = new List<PaymentGroup>();
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
