using BlazorAdminTemplate.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class StaffTrainingClassGetDTO
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? WeekNumber { get; set; }
        public int? FromWeekNumber { get; set; }
        public int? ToWeekNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TrainingClassTypesGUID { get; set; }
        public string? TrainingClassLocationGUID { get; set; }
    }

    public class StaffTrainingClassGetResponseDTO
    {
        public string TrainingClassClassesGUID { get; set; } = string.Empty;
        public string TrainingClassClassesName { get; set; } = string.Empty;
        public string TrainingClassClassesDescription { get; set; } = string.Empty;
        public string TrainingClassTypesGUID { get; set; } = string.Empty;
        public string TrainingClassTypesName { get; set; } = string.Empty;
        public string TrainingClassLocationGUID { get; set; } = string.Empty;
        public string TrainingClassLocationName { get; set; } = string.Empty;
        public int TrainingClassClassesAccess { get; set; }
        public string[] TrainingClassClassesAccessGUIDs { get; set; } = [];
        public int TrainingClassClassesMax { get; set; }
        public int TrainingClassClassesMin { get; set; }
        public int TrainingClassClassesAutomaticCancellation { get; set; }
        public int TrainingClassClassesMaxCancellationTime { get; set; }
        public int TrainingClassClassesFine { get; set; }
        public DateTime TrainingClassClassesStartDateTime { get; set; }
        public DateTime TrainingClassClassesEndDateTime { get; set; }
        public int TrainingClassClassesDuration { get; set; }
        public string? TrainingClassTemplateTrainerGUID { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public int CurrentEnrollment { get; set; }
        public int AvailableSlots { get; set; }
    }

    public class StaffTrainingClassGetListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string AppliedFilter { get; set; } = string.Empty;
        public PaginationResponseDTO Pagination { get; set; } = new PaginationResponseDTO();
        public int Count { get; set; }
        public List<StaffTrainingClassGetResponseDTO> Classes { get; set; } = [];
    }
}
