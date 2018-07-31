using Scheduals.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals
{
    public class Schedule
    {
        static int counter = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public bool ISDisposed { get; set; }
        public TimeSpan DelayRunFor { get; set; }
        public bool IsRunOnce { get; set; }
        public List<IJob> Jops { get; set; }
        public DateTime RunTime { get; set; }
        internal Func<DateTime, DateTime> CalculateNextRun { get; set; }
       

        public Schedule()
        {
            Id = counter++;
            ISDisposed = false;
            IsRunOnce = false;
            Jops = new List<IJob>();
            State = "Waiting!!!!!!!!!!!!!!";
        }
        public Schedule stope()
        {
            ISDisposed = true;
            return this;
        }
        public Schedule Run()
        {
            ISDisposed = false;
            return this;
        }
        public Schedule WithName(string name)
        {
            Name = name;
            return this;
            
        }
        public Schedule Addjop(IJob jop)
        {
            this.Jops.Add(jop);
            return this;
        }
        public Units.Units RunEvery(int i)
        {
            return new Units.Units(this,i);
        }
        public Units.Units ToRunOnceIn(int interval)
        {
            IsRunOnce = true;
            return new Units.Units(this, interval);
        }
    }
}
