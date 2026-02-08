using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class BookClassDetails
    {
        public string MemberClassesBookClassClassName { get; set; } = string.Empty;
        public DateTime MemberClassesBookClassStartDateTime { get; set; }
        public int MemberClassesBookClassDuration { get; set; }
        public string MemberClassesBookClassTrainerName { get; set; } = string.Empty;
    }
}
