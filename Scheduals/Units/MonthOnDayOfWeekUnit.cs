﻿using Scheduals.Extintion;
using Scheduals;
using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
   public class MonthOnDayOfWeekUnit: IUnitType
    {
        private readonly int _duration;

        private readonly Week _week;

        private readonly DayOfWeek _dayOfWeek;

        internal MonthOnDayOfWeekUnit(Schedule schedule, int duration, Week week, DayOfWeek dayOfWeek)
        {
            _duration = duration;
            _week = week;
            _dayOfWeek = dayOfWeek;
            Schedule = schedule;
            At(0, 0);
        }

        public Schedule Schedule { get; set; }

        /// <summary>
        /// Runs the job at the given time of day.
        /// </summary>
        /// <param name="hours">The hours (0 through 23).</param>
        /// <param name="minutes">The minutes (0 through 59).</param>

        public IUnitType At(int hours, int minutes)
        {
            if (_week == Week.Last)
            {
                Schedule.CalculateNextRun = x =>
                {
                    var nextRun = x.Date.First().Last(_dayOfWeek).AddHours(hours).AddMinutes(minutes);
                    return x > nextRun ? x.Date.First().AddMonths(_duration).Last(_dayOfWeek).AddHours(hours).AddMinutes(minutes) : nextRun;
                };
            }
            else
            {
                Schedule.CalculateNextRun = x =>
                {
                    var nextRun = x.Date.First().ToWeek(_week).ThisOrNext(_dayOfWeek).AddHours(hours).AddMinutes(minutes);
                    return x > nextRun ? x.Date.First().AddMonths(_duration).ToWeek(_week).ThisOrNext(_dayOfWeek).AddHours(hours).AddMinutes(minutes) : nextRun;
                };
            }
            return this;
        }
    }
}
