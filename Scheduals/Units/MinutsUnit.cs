using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduals;

namespace Scheduals.Units
{
    public class MinutsUnit : IUnitType
    {
        private int duartion;

        public MinutsUnit(Schedule Schedule, int duration)
        {
            this.Schedule = Schedule;
            this.duartion = duration < 1 ? 1 : duration;
            this.Schedule.CalculateNextRun = x =>
            {
                
                return x.AddMinutes(duartion);
            };
        }

        public Schedule Schedule { get ; set; }

        public DateTime IncrimentDate(DateTime d)
        {
            throw new NotImplementedException();
        }
    }
}
