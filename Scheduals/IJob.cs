using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduals
{
  public  interface IJob
    {
        object Args { get; set; }
        void Excute();
    }
}
