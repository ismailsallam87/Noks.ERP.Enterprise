using Scheduals.Extintion;
using Scheduals;
using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
   public class MonthOnLastDayOfMonthUnit: IUnitType
    {
        private readonly int _duration;

        internal MonthOnLastDayOfMonthUnit(Schedule schedule, int duration)
        {
            _duration = duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.Last();
                return x > nextRun ? x.Date.First().AddMonths(_duration).Last() : x.Date.Last();
            };
        }

        public Schedule Schedule { get;  set; }

        /// <summary>
        /// Runs the job at the given time of day.
        /// </summary>
        /// <param name="hours">The hours (0 through 23).</param>
        /// <param name="minutes">The minutes (0 through 59).</param>
        public IUnitType At(int hours, int minutes)
        {
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.Last().AddHours(hours).AddMinutes(minutes);
                return x > nextRun ? x.Date.First().AddMonths(_duration).Last().AddHours(hours).AddMinutes(minutes) : nextRun;
            };
            return this;
        }
    }
}
