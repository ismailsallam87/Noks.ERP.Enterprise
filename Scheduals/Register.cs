using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals
{
  public  class Register
    {
        public Register()
        {
            scedualList = new List<Schedule>();
            ItemJob = new List<Scheduals.ItemJob>();
        }
        public List<Schedule> scedualList { get; private set; }
        public List<ItemJob> ItemJob { get; set; }
        public void AddSchedual(Schedule schedual)
        {
            scedualList.Add(schedual);
        }
    }
    public class ItemJob
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IJob Jop { get; set; }
    }
}
