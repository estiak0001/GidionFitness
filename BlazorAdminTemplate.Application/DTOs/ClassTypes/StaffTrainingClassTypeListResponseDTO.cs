using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.ClassTypes
{
    public class StaffTrainingClassTypeListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; } = 0;

        public List<StaffTrainingClassTypeDTO> types { get; set; } = new List<StaffTrainingClassTypeDTO>();



    }
}
