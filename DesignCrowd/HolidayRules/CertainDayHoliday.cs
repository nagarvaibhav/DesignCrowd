using DesignCrowd.Infrastructure;
using DesignCrowd.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignCrowd.HolidayRules
{
    /// <summary>
    /// Public holidays on a certain occurrence of a certain day in a month
    /// </summary>
    public class CertainDayHoliday : IHolidayRule
    {
        private List<Holiday> Holidays = new List<Holiday>
        {
            new Holiday{ Week = 2, DayOfWeek = DayOfWeek.Monday, Month = 6 }
        };

        public IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData)
        {
            var holidays = new List<DateTime>();
            while (firstDate.Date < secondData.Date)
            {
                var week = firstDate.GetWeekOfMonth();
                if (Holidays.Any(d => week == d.Week && d.DayOfWeek == firstDate.DayOfWeek && d.Month == firstDate.Month))
                {
                    holidays.Add(new DateTime(firstDate.Year, firstDate.Month, firstDate.Day));
                }
                firstDate = firstDate.AddDays(1);
            }

            return holidays;
        }
    }
}