﻿using Scheduals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
    public sealed class SpecificTimeUnit
    {
        internal SpecificTimeUnit(Schedule schedule)
        {
            Schedule = schedule;
        }

        internal Schedule Schedule { get; private set; }

        /// <summary>
        /// Also runs the job according to the given interval.
        /// </summary>
        /// <param name="interval">Interval to wait.</param>
        //public Units AndEvery(int interval)
        //{
        //    var parent = Schedule.Parent ?? Schedule;

        //    var child =
        //        new Schedule(Schedule.Jobs)
        //        {
        //            Parent = parent,
        //            Reentrant = parent.Reentrant,
        //            Name = parent.Name,
        //        };

        //    if (parent.CalculateNextRun != null)
        //    {
        //        var now = JobManager.Now;
        //        var delay = parent.CalculateNextRun(now) - now;

        //        if (delay > TimeSpan.Zero)
        //            child.DelayRunFor = delay;
        //    }

        //    child.Parent.AdditionalSchedules.Add(child);
        //    return child.ToRunEvery(interval);
        //}
    }
}
