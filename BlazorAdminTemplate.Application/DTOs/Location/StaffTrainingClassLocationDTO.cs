using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Location
{
    public class StaffTrainingClassLocationDTO
    {
        [JsonPropertyName("trainingClassLocationGUID")]
        public string TrainingClassLocationGUID { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassLocationName")]
        public string TrainingClassLocationName { get; set; } = string.Empty;

        public StaffTrainingClassLocationDTO()
        {
            
        }

        public StaffTrainingClassLocationDTO(StaffTrainingClassLocationDTO locationDTO)
        {
            TrainingClassLocationGUID = locationDTO.TrainingClassLocationGUID;
            TrainingClassLocationName = locationDTO.TrainingClassLocationName;
        }

    }
}
