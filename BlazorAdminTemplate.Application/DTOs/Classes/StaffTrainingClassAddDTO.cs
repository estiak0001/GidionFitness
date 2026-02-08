using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class StaffTrainingClassAddDTO
    {
        [JsonPropertyName("trainingClassTypesGUID")]
        public string TrainingClassTypesGUID { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassLocationGUID")]
        public string TrainingClassLocationGUID { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassClassesName")]
        public string TrainingClassClassesName { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassClassesDescription")]
        public string TrainingClassClassesDescription { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassClassesAccess")]
        public int TrainingClassClassesAccess { get; set; }

        [JsonPropertyName("trainingClassClassesAccessGUIDs")]
        public string[] TrainingClassClassesAccessGUIDs { get; set; } = [];

        [JsonPropertyName("trainingClassClassesMax")]
        public int TrainingClassClassesMax { get; set; }

        [JsonPropertyName("trainingClassClassesMin")]
        public int TrainingClassClassesMin { get; set; }

        [JsonPropertyName("trainingClassClassesAutomaticCancellation")]
        public int TrainingClassClassesAutomaticCancellation { get; set; }

        [JsonPropertyName("trainingClassClassesMaxCancellationTime")]
        public int TrainingClassClassesMaxCancellationTime { get; set; }

        [JsonPropertyName("trainingClassClassesFine")]
        public int TrainingClassClassesFine { get; set; }

        [JsonPropertyName("trainingClassClassesStartDateTime")]
        public DateTime TrainingClassClassesStartDateTime { get; set; }

        [JsonPropertyName("trainingClassClassesDuration")]
        public int TrainingClassClassesDuration { get; set; }

        [JsonPropertyName("trainingClassTemplateTrainerGUID")]
        public string TrainingClassTemplateTrainerGUID { get; set; } = string.Empty;
        public StaffTrainingClassAddDTO()
        {
        }
    }

    public class StaffTrainingClassEditDTO
    {
        public string TrainingClassClassesGUID { get; set; } = string.Empty;
        public string TrainingClassTypesGUID { get; set; } = string.Empty;

        public string TrainingClassLocationGUID { get; set; } = string.Empty;

        public string TrainingClassClassesName { get; set; } = string.Empty;

        public string TrainingClassClassesDescription { get; set; } = string.Empty;

        public int TrainingClassClassesAccess { get; set; }

        public string[] TrainingClassClassesAccessGUIDs { get; set; } = [];

        public int TrainingClassClassesMax { get; set; }

        public int TrainingClassClassesMin { get; set; }

        public int TrainingClassClassesAutomaticCancellation { get; set; }

        public int TrainingClassClassesMaxCancellationTime { get; set; }

        public int TrainingClassClassesFine { get; set; }

        public DateTime TrainingClassClassesStartDateTime { get; set; }

        public int TrainingClassClassesDuration { get; set; }

        public string TrainingClassTemplateTrainerGUID { get; set; } = string.Empty;
    }

    public class StaffTrainingClassCancelResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string TrainingClassClassesGuid { get; set; } = string.Empty;
        public int EnrolledMembersCount { get; set; }
        public int EmailsSent { get; set; }
        public int EmailsFailed { get; set; }
        public List<CancelEmailResultsDTO> EmailResults { get; set; } = new List<CancelEmailResultsDTO>();
    }

    public class StaffTrainingClassActionResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class CancelEmailResultsDTO
    {
        public string MemberGuid { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Success { get; set; }
    }

}
