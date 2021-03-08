using System;
using System.Linq;
using System.Collections.Generic;
using DesignCrowd.Infrastructure;
using DesignCrowd.Model;

namespace DesignCrowd.HolidayRules
{
    /// <summary>
    /// Public holidays which are always on the same day, except when that falls on a weekend
    /// </summary>
    public class SameDayExceptWeekendHoliday : IHolidayRule
    {
        private List<Holiday> Holidays = new List<Holiday>
        {
            new Holiday{ Day = 1, Month = 1 },
            new Holiday{ Day = 26, Month = 1 },
            new Holiday{ Day = 25, Month = 12 },
            new Holiday{ Day = 26, Month = 12 }
        };

        public IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData)
        {
            var holidays = new List<DateTime>();
            while (firstDate.Date < secondData.Date)
            {
                if (Holidays.Any(d => d.Day == firstDate.Day && d.Month == firstDate.Month))
                {
                    DateTime d = firstDate;
                    while (true)
                    {
                        if (!d.IsWeekend() && !holidays.Any(x => x.Date == d.Date))
                        {
                            holidays.Add(new DateTime(d.Year, d.Month, d.Day));
                            break;
                        }
                        d = d.AddDays(1);
                    }
                }
                firstDate = firstDate.AddDays(1);
            }
            return holidays;
        }
    }
}