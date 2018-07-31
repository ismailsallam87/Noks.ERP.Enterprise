using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity.CustomClaims
{
    [Flags]
    public enum RolsEnum
    {
        NokAdministrators=1,
            NokHr= 1 << 1,
        NokEmployee= 1 << 2,
    }
}
