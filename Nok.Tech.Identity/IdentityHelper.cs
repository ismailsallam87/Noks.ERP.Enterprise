using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Nok.Tech.Identity.CustomClaims;
using Nok.Tech.Identity.Managers;
using Nok.Tech.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity
{
   public class IdentityHelper
    {
        public static void SeedIdentities(NokIdentityContext context)
        {
            var UserManger = new NokUserMangare(new UserStore<NokUser>(context));
            var RolManger = new NokRoleManager(new MRoleStore(context));
          //  context.Users
            RolManger.EnsureDefaultRolesExist();
            CreatUser(UserManger, "Admin@nokSmart.com", "Admin");

        }
        private static void CreatUser(NokUserMangare UserManger, string Mail, string FullName)
        {
            NokUser u = UserManger.FindByName(Mail);
            if (u == null)
            {

                u = new NokUser()
                {
                    DisplayName = FullName,
                    UserName = Mail,
                    Email = Mail,
                    EmailConfirmed = true
                   
                };

                IdentityResult userResult = UserManger.Create(u, SystemBasicData.DefualtPassowred());
                if (userResult.Succeeded)
                {
                    var result = UserManger.AddToRole(u.Id, RolsEnum.NokAdministrators.ToString());
                }

            }
        }

    }
    public class MRoleStore : RoleStore<Nok.Tech.Identity.Model.NokIdentotyRol>
    {
        public MRoleStore(NokIdentityContext context) : base(context)
        {
        }
    }
}
