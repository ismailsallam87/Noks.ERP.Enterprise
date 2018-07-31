using Microsoft.AspNet.Identity;
using Nok.Tech.Identity.CustomClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


    public static class NokExtintion
    {
        
           
            public static string GetUserDisplayName(this IIdentity identity)
            {
                if (identity == null)
                {
                    throw new ArgumentNullException("identity");
                }
                var ci = identity as ClaimsIdentity;
                if (ci != null)
                {
                    return ci.FindFirstValue(CustClaimType.DispalyName);
                }
                return null;
            }
        }
   