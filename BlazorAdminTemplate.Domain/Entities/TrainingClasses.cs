using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class TrainingClasses
    {
        public string TrainingClassClassesGUID { get; set; } = string.Empty;
        public string TrainingClassClassesName { get; set; } = string.Empty;
        public string TrainingClassClassesDescription { get; set; } = string.Empty;
        public int TrainingClassClassesMax { get; set; }
        public int TrainingClassClassesMin { get; set; }
        public int TrainingClassClassesFine { get; set; }
        public int TrainingClassClassesCurrentEnrollment { get; set; }
        public int TrainingClassClassesAvailableSlots { get; set; }
        public DateTime TrainingClassClassesStartDateTime { get; set; }
        public DateTime TrainingClassClassesEndDateTime { get; set; }
        public int TrainingClassClassesDuration { get; set; }
        public int TrainingClassClassesDaysOfWeek { get; set; }
        public string TrainingClassClassesTrainerName { get; set; } = string.Empty;
        public string TrainingClassClassesTrainerGUID { get; set; } = string.Empty;
        public string OrganisationSubGUID { get; set; } = string.Empty;
        public string OrganisationSubName { get; set; } = string.Empty;
        public string TrainingClassTypesGUID { get; set; } = string.Empty;
        public string TrainingClassTypesName { get; set; } = string.Empty;
    }
}
