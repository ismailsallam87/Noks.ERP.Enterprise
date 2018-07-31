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
   public class HourUnit: IUnitType
    {
        private readonly int _duration;

        internal HourUnit(Schedule schedule, int duration)
        {
            _duration = duration < 1 ? 1 : duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.AddHours(_duration);
                return x > nextRun ? nextRun.AddHours(_duration) : nextRun;
            };
        }

        public Schedule Schedule { get;  set; }

       

        /// <summary>
        /// Runs the job at the given minute of the hour.
        /// </summary>
        /// <param name="minutes">The minutes (0 through 59).</param>
        public IUnitType At(int minutes)
        {
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.ClearMinutesAndSeconds().AddMinutes(minutes);
                return _duration == 1 && x < nextRun ? nextRun : nextRun.AddHours(_duration);
            };
            return this;
        }
    }
}
