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
  public  class MonthOnDayOfMonthUnit: IUnitType
    {
        private readonly int _duration;

        private readonly int _dayOfMonth;

        internal MonthOnDayOfMonthUnit(Schedule schedule, int duration, int dayOfMonth)
        {
            _duration = duration;
            _dayOfMonth = dayOfMonth;
            Schedule = schedule;
            At(0, 0);
        }

        public Schedule Schedule { get;  set; }

      

        DateTime DayIncrement(DateTime increment)
        {
            return increment.AddDays(_duration);
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
                Func<DateTime, DateTime> calculate = y =>
                {
                    var day = Math.Min(_dayOfMonth, DateTime.DaysInMonth(y.Year, y.Month));
                    return y.AddDays(day - 1).AddHours(hours).AddMinutes(minutes);
                };

                var date = x.Date.First();
                var runThisMonth = calculate(date);
                var runAfterThisMonth = calculate(date.AddMonths(_duration));

                return x > runThisMonth ? runAfterThisMonth : runThisMonth;
            };

            return this;
        }
    }
}
