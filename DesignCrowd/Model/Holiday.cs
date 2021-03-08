using System;

namespace DesignCrowd.Model
{
    public class Holiday
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int Week { get; set; }
    }
}
