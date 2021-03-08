using System;
using System.Linq;
using System.Collections.Generic;
using DesignCrowd.Model;

namespace DesignCrowd.HolidayRules
{
    /// <summary>
    /// Public holidays which are always on the same day
    /// </summary>
    public class SameDayHoliday : IHolidayRule
    {
        private List<Holiday> Holidays = new List<Holiday>
        {
            new Holiday{ Day = 25, Month = 4 },
            new Holiday{ Day = 25, Month = 12 },
            new Holiday{ Day = 26, Month = 1 }
        };

        public IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData)
        {
            var holidays = new List<DateTime>();
            while (firstDate.Date < secondData.Date)
            {
                if (Holidays.Any(d => d.Day == firstDate.Day && d.Month == firstDate.Month))
                {
                    holidays.Add(new DateTime(firstDate.Year, firstDate.Month, firstDate.Day));
                }
                firstDate = firstDate.AddDays(1);
            }

            return holidays;
        }
    }
}