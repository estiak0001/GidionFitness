
using BlazorAdminTemplate.Domain.Enums;

namespace BlazorAdminTemplate.Pages.AccessControllerGroup
{
    public class DayScheduleViewModel
    {
        public string Id { get; set; } = string.Empty;
        public int DayNumber { get; set; }
        public bool IsEnabled { get; set; }
        public string DayName => ((CustomDayOfWeek)DayNumber).ToString();
        public List<TimeSlotViewModel> TimeSlots { get; set; } = new();
    }

    public class TimeSlotViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string DayConfigId { get; set; } = string.Empty;
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool IsNew { get; set; } = false;
    }
}

