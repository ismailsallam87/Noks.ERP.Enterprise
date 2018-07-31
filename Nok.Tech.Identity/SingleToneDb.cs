using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nok.Tech.Identity
{
  public  class SingleToneDb
    {
        public static NokIdentityContext Get()
        {
            return (NokIdentityContext)(HttpContext.Current.Items["DataContext"] ?? (HttpContext.Current.Items["DataContext"] = new NokIdentityContext()));

        }
    }
}
