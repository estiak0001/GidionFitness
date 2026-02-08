using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.ClassTypes
{
    public class StaffTrainingClassTypeDeleteResponseDTO
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("trainingClassTypesGUID")]
        public string TrainingClassTypesGUID { get; set; } = string.Empty;
    }
}
