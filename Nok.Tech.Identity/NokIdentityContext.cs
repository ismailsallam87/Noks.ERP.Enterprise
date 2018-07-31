using Microsoft.AspNet.Identity.EntityFramework;
using Nok.Tech.Identity.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity
{
   public class NokIdentityContext: IdentityDbContext<NokUser,NokIdentotyRol,string,IdentityUserLogin,IdentityUserRole,IdentityUserClaim>
    {
        public NokIdentityContext():base("NokIdentityContext")
        {

        }
        protected override void OnModelCreating(DbModelBuilder builder)
        {



            base.OnModelCreating(builder);
         //  builder.Entity<NokUser>().ToTable("AspNetUsers");
            builder.Entity<NokIdentotyRol>().ToTable("NokIdentityRoles");
            builder.Entity<IdentityRole>().ToTable("NokIdentityRoles");
            builder.Entity<IdentityUserRole>().ToTable("NokIdentityUserRoles");
            builder.Entity<IdentityUserLogin>().ToTable("NokIdentityUserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("NokIdentityUserClaims");
        }
    }
   
}
