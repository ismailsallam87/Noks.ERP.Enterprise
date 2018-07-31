using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals.Units
{
    public class DayUnit : IUnitType
    {
        private int duartion;

        public DayUnit(Schedule Schedule, int duration)
        {
            this.Schedule = Schedule;
            this.duartion = duration<1?1:duration;
            this.Schedule.CalculateNextRun = x =>
            {
                var nextRun = x.Date.AddDays(duration);
                return x>nextRun?this.IncrimentDate(x):nextRun;
            };
        }

        public IUnitType At(int hour,int minut)
        {
            Schedule.CalculateNextRun = x => {
                var nextRun = x.Date.AddHours(hour).AddMinutes(minut);
                return x > nextRun ? this.IncrimentDate(x) : nextRun;
            };
            return this;
        }

        public DateTime IncrimentDate(DateTime d)
        {
         return   d.Date.AddDays(duartion);
        }

        public Schedule Schedule { get ;  set ; }
    }
}
