using Microsoft.AspNet.Identity.EntityFramework;
using Nok.Tech.Identity.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity.Model
{
   public class NokIdentotyRol: IdentityRole
    {
        public NokIdentotyRol()
        {

        }
        public NokIdentotyRol(string name)
        {
            Name = name;
        }
        //[Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(System.Data.Entity.Validation))]
        //[StringLength(100, ErrorMessageResourceName = "FieldMaximumLength", ErrorMessageResourceType = typeof(System.Data.Entity.Validation))]
        //[Display(Name = "DisplayName")]
        //public string DisplayName
        //{
        //    get
        //    {
        //        if (NokUserMangare.DefaultRolesDisplayName.ContainsKey(this.Name))
        //            return NokUserMangare.DefaultRolesDisplayName[this.Name];
        //        else
        //            return this.displayName;
        //    }
        //    set
        //    {
        //        this.displayName = value;
        //    }
        //}


        //[StringLength(500, ErrorMessageResourceName = "FieldMaximumLength", ErrorMessageResourceType = typeof(Validation))]
        //[Display(Name = "RoleDescription", ResourceType = typeof(Strings))]
        //public string Description
        //{
        //    get
        //    {
        //        if (NokUserMangare.DefaultRolesDescription.ContainsKey(this.Name))
        //            return NokUserMangare.DefaultRolesDescription[this.Name];
        //        else
        //            return this.displayName;
        //    }
        //    set
        //    {
        //        this.description = value;
        //    }
        //}
    }
}
