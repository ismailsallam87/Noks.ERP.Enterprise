using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Nok.Tech.Identity.Model;

namespace Nok.Tech.Identity
{
    internal class NokUserStore : UserStore<NokUser>
    {
        private NokIdentityContext context;

        public NokUserStore(NokIdentityContext context)
        {
            this.context = context;
        }
    }
}