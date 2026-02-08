using BlazorAdminTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.DTOs.Access
{
    public class StaffScheduleTimeSlotDTO
    {
        public string AccessControllerScheduleTimeSlotGUID { get; set; } = string.Empty;
    }

    public class StaffScheduleTimeSlotListResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string DayConfigGuid { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<ScheduleTimeSlot> ScheduleTimeSlots { get; set; } = new List<ScheduleTimeSlot>();
    }

    public class StaffScheduleTimeSlotResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleTimeSlot Data { get; set; } = new ScheduleTimeSlot();
    }

    public class StaffScheduleTimeSlotCreateDTO
    {
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
        public TimeSpan AccessControllerScheduleTimeSlotOpenTime { get; set; }
        public TimeSpan AccessControllerScheduleTimeSlotCloseTime { get; set; }
    }

    public class StaffScheduleTimeSlotUpdateDTO
    {
        public string AccessControllerScheduleTimeSlotGUID { get; set; } = string.Empty;
        public string AccessControllerScheduleDayConfigGUID { get; set; } = string.Empty;
        public TimeSpan AccessControllerScheduleTimeSlotOpenTime { get; set; }
        public TimeSpan AccessControllerScheduleTimeSlotCloseTime { get; set; }
    }

    public class StaffScheduleTimeSlotCreateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleTimeSlot Data { get; set; } = new ScheduleTimeSlot();
    }

    public class StaffScheduleTimeSlotUpdateResponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public ScheduleTimeSlot Data { get; set; } = new ScheduleTimeSlot();
    }

    public class StaffScheduleTimeSlotDeleteResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
