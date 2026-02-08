using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class BookedClasses
    {
        public string ClassGUID { get; set; } = string.Empty;
        public string TrainingClassClassesName { get; set; } = string.Empty;
        public string TrainingClassClassesDescription { get; set; } = string.Empty;
        public int TrainingClassClassesMax { get; set; }
        public int TrainingClassClassesMin { get; set; }
        public DateTime TrainingClassClassesStartDateTime { get; set; }
        public int TrainingClassClassesDuration { get; set; }
        public string TrainingClassClassesTrainerName { get; set; } = string.Empty;
        public string TrainingClassClassesTrainerGUID { get; set; } = string.Empty;
        public string TrainingClassTypesName { get; set; } = string.Empty;
        public string TrainingClassTypesDescription { get; set; } = string.Empty;
        public bool TrainingClassClassesIsEnrolled { get; set; }
    }
}
