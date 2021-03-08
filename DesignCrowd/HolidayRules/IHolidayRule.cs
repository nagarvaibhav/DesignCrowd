using System;
using System.Collections.Generic;

namespace DesignCrowd.HolidayRules
{
    public interface IHolidayRule
    {
        IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData);
    }
}