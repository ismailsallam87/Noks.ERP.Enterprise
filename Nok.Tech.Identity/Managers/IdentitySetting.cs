﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity.Managers
{
    public class IdentitySettings
    {
        public IdentitySettings()
        {
            MinimumPasswordLength = 6;
            RequireNonLetterOrDigit = true;
            RequireDigit = true;
            RequireLowercase = true;
            RequireUppercase = true;
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            AllowOnlyAlphanumericUserNames = false;
            RequireUniqueEmail = true;
        }

        public bool AllowOnlyAlphanumericUserNames { get; set; }
        public bool RequireUniqueEmail { get; set; }
        public int MinimumPasswordLength { get; set; }
        public bool RequireNonLetterOrDigit { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool UserLockoutEnabledByDefault { get; set; }
        public TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }
    }
}
