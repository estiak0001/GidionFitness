using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class ScheduleExceptions
    {
        public string AccessControllerScheduleExceptionGUID { get; set; } = string.Empty;
        public string AccessControllerAccessGroupsGUID { get; set; } = string.Empty;
        public string AccessControllerScheduleExceptionName { get; set; } = string.Empty;
        public string AccessControllerScheduleExceptionDescription { get; set; } = string.Empty;
        public DateTime AccessControllerScheduleExceptionFromDateTime { get; set; }
        public DateTime AccessControllerScheduleExceptionToDateTime { get; set; }
        public int AccessControllerScheduleExceptionAccessStatus { get; set; }
        public bool AccessControllerScheduleExceptionEnabled { get; set;  }

    }
}
