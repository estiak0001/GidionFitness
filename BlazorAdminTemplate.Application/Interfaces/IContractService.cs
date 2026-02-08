using BlazorAdminTemplate.Application.DTOs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IContractService
    {
        Task<MemberContractBeforeSigningResponseDTO> GetMemberContractBeforeSigningAsync();
        Task<MemberContractSigningResponseDTO> PostMemberContractSigningAsync(MemberContractSigningRequestDTO request);
    }
}
