using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Location
{
    public class StaffTrainingClassLocationListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();

        public int Count { get; set; } = 0;
        [JsonPropertyName("locations")]
        public List<StaffTrainingClassLocationDTO> StaffTrainingClassLocations { get; set; } = new List<StaffTrainingClassLocationDTO>();

    }
}
