using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Training
{
    public class AccessLogsListReponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public AccessLogsPaginationResponseDTO Pagination { get; set; } = new AccessLogsPaginationResponseDTO();

        [JsonPropertyName("accessLogs")]
        public List<AccessLogsDTO> AccessLogs { get; set; } = new List<AccessLogsDTO>();
    }
}
