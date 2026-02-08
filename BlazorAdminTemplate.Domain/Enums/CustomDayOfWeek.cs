using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Domain.Enums
{
    public enum CustomDayOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public static class DayOfWeekExtensions
    {
        public static CustomDayOfWeek ToWeekDayNumber(this DayOfWeek day)
            => day switch
            {
                DayOfWeek.Monday => CustomDayOfWeek.Monday,
                DayOfWeek.Tuesday => CustomDayOfWeek.Tuesday,
                DayOfWeek.Wednesday => CustomDayOfWeek.Wednesday,
                DayOfWeek.Thursday => CustomDayOfWeek.Thursday,
                DayOfWeek.Friday => CustomDayOfWeek.Friday,
                DayOfWeek.Saturday => CustomDayOfWeek.Saturday,
                DayOfWeek.Sunday => CustomDayOfWeek.Sunday,
                _ => throw new ArgumentOutOfRangeException(nameof(day))
            };

        public static DayOfWeek ToDayOfWeek(this CustomDayOfWeek weekDay)
            => weekDay switch
            {
                CustomDayOfWeek.Monday => DayOfWeek.Monday,
                CustomDayOfWeek.Tuesday => DayOfWeek.Tuesday,
                CustomDayOfWeek.Wednesday => DayOfWeek.Wednesday,
                CustomDayOfWeek.Thursday => DayOfWeek.Thursday,
                CustomDayOfWeek.Friday => DayOfWeek.Friday,
                CustomDayOfWeek.Saturday => DayOfWeek.Saturday,
                CustomDayOfWeek.Sunday => DayOfWeek.Sunday,
                _ => throw new ArgumentOutOfRangeException(nameof(weekDay))
            };
    }
}
