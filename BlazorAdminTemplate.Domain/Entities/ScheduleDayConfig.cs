using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class ScheduleDayConfig
    {
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
        public int AccessControllerScheduleDayConfigDayOfWeek { get; set; }
        public bool AccessControllerScheduleDayConfigEnabled {get; set; } 
    }
}
