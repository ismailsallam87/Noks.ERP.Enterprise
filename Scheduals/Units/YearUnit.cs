using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduals;

namespace Scheduals.Units
{
    public class YearUnit : IUnitType
    {
        private readonly int _duration;

        internal YearUnit(Schedule schedule, int duration)
        {
            _duration = duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.AddYears(_duration);
                return x > nextRun ? nextRun.AddYears(_duration) : nextRun;
            };
        }
        public Schedule Schedule { get ; set ; }
        public YearOnDayOfYearUnit On(int day)
        {
            return new YearOnDayOfYearUnit(Schedule, _duration, day);
        }

        /// <summary>
        /// Runs the job on the last day of the year.
        /// </summary>
        public YearOnLastDayOfYearUnit OnTheLastDay()
        {
            return new YearOnLastDayOfYearUnit(Schedule, _duration);
        }

    }
}
