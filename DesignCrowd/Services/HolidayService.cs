using DesignCrowd.HolidayRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignCrowd.Services
{
    public interface IHolidayService
    {
        IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData);
    }

    public class HolidayService : IHolidayService
    {
        private readonly IEnumerable<IHolidayRule> _rules;
        public HolidayService(IEnumerable<IHolidayRule> rules)
        {
            _rules = rules;
        }

        public IList<DateTime> GetHolidays(DateTime firstDate, DateTime secondData)
        {
            var holidays = new List<DateTime>();
            //Same Holidays will be added here
            foreach (var rule in _rules)
            {
                holidays.AddRange(rule.GetHolidays(firstDate, secondData));
            }
            return holidays.Select(x => x.Date).Distinct().ToList();
        }
    }
}