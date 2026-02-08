using BlazorAdminTemplate.Application.DTOs.Pagination;
using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Contracts
{
    public class StaffShowSignedMemberContractResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        [JsonPropertyName("contracts")]
        public List<ContractsPdf> SignedContracts { get; set; } = new List<ContractsPdf>();
    }
}
