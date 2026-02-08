using BlazorAdminTemplate.Application.DTOs.Members;
using BlazorAdminTemplate.Application.DTOs.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IMemberService
    {
        Task<StaffMemberListResponseDTO> GetMemberListAsync(int page, int pageSize, string search, string searchMembership);
        Task<StaffUpdateMemberResponseDTO> UpdateMemberAsync(StaffUpdateMemberRequestDTO request);
        Task<StaffGetSpecificMemberResponseDTO> GetSpecificMemberAsync(string memberGuid);
        
    }
}
