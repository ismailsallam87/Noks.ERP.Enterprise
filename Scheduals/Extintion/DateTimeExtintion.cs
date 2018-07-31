using Scheduals.Units;
using Scheduals;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Extintion
{
    internal static class DateTimeExtensions
    {
        internal static DateTime First(this DateTime current)
        {
            return current.AddDays(1 - current.Day);
        }

        internal static DateTime FirstOfYear(this DateTime current)
        {
            return new DateTime(current.Year, 1, 1);
        }

        internal static DateTime Last(this DateTime current)
        {
            var daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
            return current.First().AddDays(daysInMonth - 1);
        }

        internal static DateTime Last(this DateTime current, DayOfWeek dayOfWeek)
        {
            var last = current.Last();
            var diff = dayOfWeek - last.DayOfWeek;

            if (diff > 0)
                diff -= 7;

            return last.AddDays(diff);
        }

        internal static DateTime ThisOrNext(this DateTime current, DayOfWeek dayOfWeek)
        {
            var offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays < 0)
                offsetDays += 7;

            return current.AddDays(offsetDays);
        }

        internal static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            var offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
                offsetDays += 7;

            return current.AddDays(offsetDays);
        }

        internal static DateTime NextNWeekday(this DateTime current, int toAdvance)
        {
            while (toAdvance >= 1)
            {
                toAdvance--;
                current = current.AddDays(1);

                while (!current.IsWeekday())
                    current = current.AddDays(1);
            }
            return current;
        }

        internal static bool IsWeekday(this DateTime current)
        {
            switch (current.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return false;
                default:
                    return true;
            }
        }

        internal static DateTime ClearMinutesAndSeconds(this DateTime current)
        {
            return current.AddMinutes(-1 * current.Minute)
                .AddSeconds(-1 * current.Second)
                .AddMilliseconds(-1 * current.Millisecond);
        }

        internal static DateTime ToWeek(this DateTime current, Week week)
        {
            switch (week)
            {
                case Week.Second:
                    return current.First().AddDays(7);
                case Week.Third:
                    return current.First().AddDays(14);
                case Week.Fourth:
                    return current.First().AddDays(21);
                case Week.Last:
                    return current.Last().AddDays(-7);
            }
            return current.First();
        }
    }
    public static class DelayForExtensions
    {
        private static DelayTimeUnit DelayFor(Schedule schedule, int interval)
        {
            return new DelayTimeUnit(schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        //public static DelayTimeUnit DelayFor(this SpecificTimeUnit unit, int interval)
        //{
        //    if (unit == null)
        //        throw new ArgumentNullException("unit");

        //    return DelayFor(unit.Schedule, interval);
        //}

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this MillisecondUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this SecondUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this MinutsUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this HourUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this Scheduals.Units.DayUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this WeekUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this MonthUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }

        /// <summary>
        /// Delays the job for the given interval.
        /// </summary>
        /// <param name="unit">The schedule being affected.</param>
        /// <param name="interval">Interval to wait.</param>
        public static DelayTimeUnit DelayFor(this YearUnit unit, int interval)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            return DelayFor(unit.Schedule, interval);
        }
    }
}
