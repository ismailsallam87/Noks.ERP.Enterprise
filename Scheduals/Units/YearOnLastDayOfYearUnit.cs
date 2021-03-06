﻿using Scheduals.Extintion;
using Scheduals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
  public  class YearOnLastDayOfYearUnit
    {
        private readonly int _duration;

        internal YearOnLastDayOfYearUnit(Schedule schedule, int duration)
        {
            _duration = duration;
            Schedule = schedule;
            At(0, 0);
        }

        internal Schedule Schedule { get; private set; }

        /// <summary>
        /// Runs the job at the given time of day.
        /// </summary>
        /// <param name="hours">The hours (0 through 23).</param>
        /// <param name="minutes">The minutes (0 through 59).</param>
        public void At(int hours, int minutes)
        {
            Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.FirstOfYear().AddMonths(11).Last().AddHours(hours).AddMinutes(minutes);
                return x > nextRun ? x.Date.FirstOfYear().AddYears(_duration).AddMonths(11).Last().AddHours(hours).AddMinutes(minutes) : nextRun;
            };
        }
    }
}
