
using Nok.Tech.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Nok.Tech.Identity.CustomClaims;

namespace Nok.Tech.Identity.Managers
{
    public class NokRoleManager : RoleManager<NokIdentotyRol>
    {
        public NokRoleManager(IRoleStore<NokIdentotyRol, string> store) : base(store)
        {
        }
      

        /// <summary>
        /// Ensures the correct set of TD standard roles exist.
        /// </summary>
        public void EnsureDefaultRolesExist()
        {
            //var roles = TdIdentityContext.DefaultRoles;
            foreach (var defaultRole in DefaultRoles)
            {
                if (!this.RoleExists(defaultRole.Name))
                {
                    this.Create(defaultRole);
                }
            }

        }


        public static IEnumerable<NokIdentotyRol> DefaultRoles
        {
            get
            {
                return new[]
                {
                    new NokIdentotyRol
                    {
                        Name = RolsEnum.NokAdministrators.ToString()
                    },
                     new NokIdentotyRol
                    {
                        Name = RolsEnum.NokHr.ToString()
                     },
                     new NokIdentotyRol
                    {
                        Name = RolsEnum.NokEmployee.ToString()
                     }
                };
            }
        }

      
    }
  
}
