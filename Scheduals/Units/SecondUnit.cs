using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduals;

namespace Scheduals.Units
{
  public  class SecondUnit: IUnitType
    {
        private int duartion;

        public SecondUnit(Schedule Schedule, int duration)
        {
            this.Schedule = Schedule;
            this.duartion = duration < 1 ? 1 : duration;
            this.Schedule.CalculateNextRun = x =>
            {
               
                return x.AddSeconds(duration);
            };
        }

        public Schedule Schedule { get ; set; }

         public DateTime IncrimentDate(DateTime d)
        {
            throw new NotImplementedException();
        }
    }
}
