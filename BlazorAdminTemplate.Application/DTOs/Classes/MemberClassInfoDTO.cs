using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Classes
{
    public class MemberClassInfoDTO
    {
    }

    public class MemberClassInfoResponseDTO
    {
        public string TrainingClassClassesName { get; set; } = string.Empty;
        public string TrainingClassClassesDescription { get; set; } = string.Empty;
        public int TrainingClassClassesMax { get; set; }
        public int TrainingClassClassesMin { get; set; }
        public int TrainingClassClassesMaxCancellationTime { get; set; }
        public int TrainingClassClassesFine { get; set; }
        public DateTime TrainingClassClassesStartDateTime { get; set; }
        public DateTime TrainingClassClassesEndDateTime { get; set; }
        public int TrainingClassClassesDuration { get; set; }
        public string TrainingClassTemplateTrainerGUID { get; set; } = string.Empty;
        public string TrainerFirstName { get; set; } = string.Empty;
        public string TrainerLastName { get; set; } = string.Empty;
        public int TrainingClassClassesAvailableSlotsUsed { get; set; }
        public int TrainingClassClassesAvailableSlots { get; set; }
    }
}
