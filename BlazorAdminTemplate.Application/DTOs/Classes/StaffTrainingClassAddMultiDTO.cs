using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class StaffTrainingClassAddMultiDTO
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

        [JsonPropertyName("trainingClassClassesDuration")]
        public int TrainingClassClassesDuration { get; set; }

        [JsonPropertyName("trainingClassTemplateTrainerGUID")]
        public string TrainingClassTemplateTrainerGUID { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassTemplateStartTime")]
        public TimeOnly TrainingClassTemplateStartTime { get; set; }

        [JsonPropertyName("trainingClassTemplatePlanningStart")]
        public DateTime TrainingClassTemplatePlanningStart { get; set; }

        [JsonPropertyName("trainingClassTemplatePlanningEnd")]
        public DateTime TrainingClassTemplatePlanningEnd { get; set; }

        [JsonPropertyName("trainingClassTemplateDayOfWeek")]
        public string TrainingClassTemplateDayOfWeek { get; set; } = string.Empty;

        [JsonPropertyName("trainingClassTemplateFrequency")]
        public int TrainingClassTemplateFrequency { get; set; }


        public StaffTrainingClassAddMultiDTO()
        {
        }
    }

    public class StaffTrainingClassAddMultiResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
