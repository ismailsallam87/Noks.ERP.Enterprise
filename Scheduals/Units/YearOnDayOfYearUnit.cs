﻿using Scheduals.Extintion;
using Scheduals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
  public  class YearOnDayOfYearUnit
    {
        private readonly int _duration;

        private readonly int _dayOfYear;

        internal YearOnDayOfYearUnit(Schedule schedule, int duration, int dayOfYear)
        {
            _duration = duration;
            _dayOfYear = dayOfYear;
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
                var nextRun = x.Date.FirstOfYear().AddDays(_dayOfYear - 1).AddHours(hours).AddMinutes(minutes);
                return x > nextRun ? x.Date.FirstOfYear().AddYears(_duration).AddDays(_dayOfYear - 1).AddHours(hours).AddMinutes(minutes) : nextRun;
            };
        }
    }
}
