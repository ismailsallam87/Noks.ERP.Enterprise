using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity
{
    public class SystemBasicData
    {

        public static string DefualtPassowred()
        {
            return ConfigurationManager.AppSettings["DefualtPass"] == null ? "P@$$w0rd" : ConfigurationManager.AppSettings["DefualtPass"];
        }

    }
}
