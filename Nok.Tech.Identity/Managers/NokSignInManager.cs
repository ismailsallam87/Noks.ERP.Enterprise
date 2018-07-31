using Nok.Tech.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin;

namespace Nok.Tech.Identity.Managers
{
    public class NokSignInManager : SignInManager<NokUser, string>
    {
        public NokSignInManager(NokUserMangare userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(NokUser user)
        {
            return user.GenerateUserIdentityAsync((NokUserMangare)UserManager);
        }

        public static NokSignInManager Create(IdentityFactoryOptions<NokSignInManager> options, IOwinContext context)
        {
            return new NokSignInManager(context.GetUserManager<NokUserMangare>(), context.Authentication);
        }
    }
}
