﻿using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduals;

namespace Scheduals.Units
{
    public class MonthUnit : IUnitType
    {
        private readonly int _duration;

        internal MonthUnit(Schedule schedule, int duration)
        {
            _duration = duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x => x.Date.AddMonths(_duration);
        }

        public Schedule Schedule { get ; set; }
        public MonthOnDayOfMonthUnit On(int day)
        {
            return new MonthOnDayOfMonthUnit(Schedule, _duration, day);
        }

        /// <summary>
        /// Runs the job on the last day of the month.
        /// </summary>
        public MonthOnLastDayOfMonthUnit OnTheLastDay()
        {
            return new MonthOnLastDayOfMonthUnit(Schedule, _duration);
        }

        /// <summary>
        /// Runs the job on the given day of week on the first week of the month.
        /// </summary>
        /// <param name="day">The day of the week.</param>
        public MonthOnDayOfWeekUnit OnTheFirst(DayOfWeek day)
        {
            return new MonthOnDayOfWeekUnit(Schedule, _duration, Week.First, day);
        }

        /// <summary>
        /// Runs the job on the given day of week on the second week of the month.
        /// </summary>
        /// <param name="day">The day of the week.</param>
        public MonthOnDayOfWeekUnit OnTheSecond(DayOfWeek day)
        {
            return new MonthOnDayOfWeekUnit(Schedule, _duration, Week.Second, day);
        }

        /// <summary>
        /// Runs the job on the given day of week on the third week of the month.
        /// </summary>
        /// <param name="day">The day of the week.</param>
        public MonthOnDayOfWeekUnit OnTheThird(DayOfWeek day)
        {
            return new MonthOnDayOfWeekUnit(Schedule, _duration, Week.Third, day);
        }

        /// <summary>
        /// Runs the job on the given day of week on the fourth week of the month.
        /// </summary>
        /// <param name="day">The day of the week.</param>
        public MonthOnDayOfWeekUnit OnTheFourth(DayOfWeek day)
        {
            return new MonthOnDayOfWeekUnit(Schedule, _duration, Week.Fourth, day);
        }

        /// <summary>
        /// Runs the job on the given day of week on the last week of the month.
        /// </summary>
        /// <param name="day">The day of the week.</param>
        public MonthOnDayOfWeekUnit OnTheLast(DayOfWeek day)
        {
            return new MonthOnDayOfWeekUnit(Schedule, _duration, Week.Last, day);
        }
    }
}
