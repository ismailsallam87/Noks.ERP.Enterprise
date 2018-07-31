using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Nok.Tech.Identity.CustomClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity.Model
{
  public  class NokUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public bool  IsWindowsUser { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<NokUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //  userIdentity.AddClaim(new Claim(ClimeType.combanyName, this.combany.Name));
           // userIdentity.AddClaim(new Claim(ClimeType.combanyID, this.CombanyId.ToString()));
            userIdentity.AddClaim(new Claim(CustClaimType.DispalyName, this.DisplayName));

            return userIdentity;
        }
    }
}
