using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class CancelClassDetails
    {
        public string ClassName { get; set; } = string.Empty;
        public DateTime ClassStartDateTime { get; set; } 
        public string TrainerName { get; set; } = string.Empty;
    }
}
