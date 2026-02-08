using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Payment
{
    public class MemberPaymentListReponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public List<MemberPaymentDTO> Payments { get; set; } = new List<MemberPaymentDTO>();
    }
}
