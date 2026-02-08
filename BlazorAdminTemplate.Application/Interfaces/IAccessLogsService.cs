using BlazorAdminTemplate.Application.DTOs.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Interfaces
{
    public interface IAccessLogsService
    {
        Task<AccessLogsListReponseDTO> GetAccessLogsAsync(int page);
        Task<MemberAccessLogsTotalResponseDTO> GetTotalAccessLogsAsync();
    }
}
