using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
   public class Units
    {
        private int i;
        public Schedule Schedule;
        public Units(Schedule Schedule, int duration)
        {
            this.i = duration;
            this.Schedule = Schedule;
        }

        public DayUnit Days()
        {
            return new DayUnit(Schedule,i);
        }
        public SecondUnit Seconds()
        {
            return new SecondUnit(Schedule, i);
        }
        public MinutsUnit Minuts()
        {
            return new MinutsUnit(Schedule, i);
        }
        public WeekdayUnit Weekdays()
        {
            return new WeekdayUnit(Schedule, i);
        }

        /// <summary>
        /// Sets the interval to weeks.
        /// </summary>
        public WeekUnit Weeks()
        {
            return new WeekUnit(Schedule, i);
        }

        /// <summary>
        /// Sets the interval to months.
        /// </summary>
        public MonthUnit Months()
        {
            return new MonthUnit(Schedule, i);
        }

        /// <summary>
        /// Sets the interval to years.
        /// </summary>
        public YearUnit Years()
        {
            return new YearUnit(Schedule, i);
        }
    }
}
