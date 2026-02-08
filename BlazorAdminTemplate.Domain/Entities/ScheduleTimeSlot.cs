using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Entities
{
    public class ScheduleTimeSlot
    {
        public string AccessControllerScheduleTimeSlotGUID { get; set; } = string.Empty;
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
        public TimeSpan AccessControllerScheduleTimeSlotOpenTime { get; set; }
        public TimeSpan AccessControllerScheduleTimeSlotCloseTime { get; set; }
        public int AccessControllerScheduleTimeSlotDayOfWeek { get; set; }

    }
}
