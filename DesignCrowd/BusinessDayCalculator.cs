using DesignCrowd.Infrastructure;
using DesignCrowd.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignCrowd
{
    public class BusinessDayCalculator
    {
        private readonly IHolidayService _holidayService;

        public BusinessDayCalculator(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var weekdayCounter = 0;

            if (firstDate >= secondDate)
                return weekdayCounter;

            firstDate = firstDate.AddDays(1);
            while (firstDate.Date < secondDate.Date)
            {
                if (!firstDate.IsWeekend())
                {
                    weekdayCounter++;
                }
                firstDate = firstDate.AddDays(1);
            }

            return weekdayCounter;
        }
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var weekdayCounter = 0;

            if (firstDate.Date >= secondDate.Date)
                return weekdayCounter;

            var startDate = firstDate.AddDays(1);
            var endDate = secondDate;

            while (startDate.Date < endDate.Date)
            {
                if (!startDate.IsWeekend())
                {
                    if (!publicHolidays.Any(x => x.Date == startDate.Date))
                        weekdayCounter++;
                }
                startDate = startDate.AddDays(1);
            }

            return weekdayCounter;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var holidays = _holidayService.GetHolidays(firstDate, secondDate);
            return BusinessDaysBetweenTwoDates(firstDate, secondDate, holidays);
        }
    }
}
