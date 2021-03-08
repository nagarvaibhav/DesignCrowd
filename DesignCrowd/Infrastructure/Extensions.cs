using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignCrowd.Infrastructure
{
    public static class Extensions
    {
        public static bool IsWeekend(this DateTime date)
        {
            var weekends = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
            return weekends.Any(day => day == date.DayOfWeek);
        }
    }
}
