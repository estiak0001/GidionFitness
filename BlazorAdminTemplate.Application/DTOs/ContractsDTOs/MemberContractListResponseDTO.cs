using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.Pagination;
using System.Text.Json.Serialization;

namespace BlazorAdminTemplate.Application.DTOs.ContractsDTOs
{
    public class MemberContractListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public MemberContractPaginationResponseDTO Pagination { get; set; } = new MemberContractPaginationResponseDTO();

        [JsonPropertyName("contracts")]

        public List<MemberContractDTO> MemberContracts { get; set; } = new List<MemberContractDTO>();
    }
}
