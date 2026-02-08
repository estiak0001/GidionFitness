using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.ContractsDTOs;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMemberContractService
    {
        Task<MemberContractListResponseDTO> GetMemberContractAsync(int page);
        Task<MemberContractTotalResponseDTO> GetTotalMemberContractsAsync();
    }
}
