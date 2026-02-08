using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Location
{
    public class StaffTrainingClassUpdateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string TrainingClassLocationGuid { get; set; } = string.Empty;
        public string TrainingClassLocationName { get; set; } = string.Empty;
    }
}
