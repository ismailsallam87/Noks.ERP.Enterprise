using Scheduals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
  public  class MillisecondUnit
    {
        private readonly int _duration;

        internal MillisecondUnit(Schedule schedule, int duration)
        {
            _duration = duration;
            Schedule = schedule;
            Schedule.CalculateNextRun = x => x.AddMilliseconds(_duration);
        }

        internal Schedule Schedule { get; private set; }
    }
}
