using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.ClassTypes
{
    public class StaffTrainingClassTypeUpdateResponseDTO
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassTypesGUID")]
        public string TrainingClassTypesGUID { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassLocationGUID")]
        public string TrainingClassLocationGUID { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassTypesName")]
        public string TrainingClassTypesName { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassTypesDescription")]
        public string TrainingClassTypesDescription { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassTypesMax")]
        public int TrainingClassTypesMax { get; set; } = 0;
    }
}
