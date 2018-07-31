using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduals;
using Scheduals.Extintion;

namespace Scheduals.Units
{
    public class WeekdayUnit : IUnitType
    {
        private readonly int _duration;

        public WeekdayUnit(Schedule schedule, int duration)
        {
            _duration = duration < 1 ? 1 : duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.NextNWeekday(_duration);
                return x > nextRun || !nextRun.Date.IsWeekday() ? nextRun.NextNWeekday(_duration) : nextRun;
            };
        }

        

        /// <summary>
        /// Runs the job at the given time of day.
        /// </summary>
        /// <param name="hours">The hours (0 through 23).</param>
        /// <param name="minutes">The minutes (0 through 59).</param>
        public IUnitType At(int hours, int minutes)
        {
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.AddHours(hours).AddMinutes(minutes);
                return x > nextRun || !nextRun.Date.IsWeekday() ? nextRun.NextNWeekday(_duration) : nextRun;
            };
            return this;
        }
        public Schedule Schedule { get; set ; }

        
    }
}
