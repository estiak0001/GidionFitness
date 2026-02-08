using BlazorAdminTemplate.Application.DTOs;
using BlazorAdminTemplate.Application.DTOs.NewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface INewsService
    {
        Task<MemberNewsShowListResponseDTO> GetMemberNewsShowListAsync(int pageNumber, int pageSize);
        Task<MemberNewsShowResponseDTO> GetSpecificMemberNewsAsync(string memberNewsGuid);
        Task<MemberNewsShowListTopResponseDTO> GetTopMemberNewsListAsync();
        Task<StaffCreateNewsResponseDTO> CreateStaffNewsAsync(StaffCreateNewsRequestDTO newsRequest);
        Task<StaffEditNewsReponseDTO> EditStaffNewsAsync(StaffEditNewsRequestDTO newsRequest);
        Task<StaffDeleteNewsResponseDTO> DeleteStaffNewsAsync(StaffDeleteNewsRequestDTO newsRequest);


        Task<ResponseDTO<List<StaffNewsDTO>>> GetStaffNewsAsync(int pageNumber, int pageSize);
        Task<ResponseDTO<StaffNewsDTO>> CreateStaffNewAsync(StaffNewsDTO news);
        Task<ResponseDTO<StaffNewsDTO>> UpdateStaffNewAsync(StaffNewsDTO news);
        Task<ResponseDTO<StaffNewsDTO>> DeleteStaffNewAsync(StaffNewsDTO news);

    }
}
