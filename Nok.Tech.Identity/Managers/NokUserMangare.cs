using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Nok.Tech.Identity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nok.Tech.Identity.Managers
{
    public class NokUserMangare : UserManager<NokUser>
    {
        public NokUserMangare(IUserStore<NokUser> store) : base(store)
        {
        }
        public static NokUserMangare Create(IdentityFactoryOptions<NokUserMangare> options, IOwinContext context)
        {
            var manager = new NokUserMangare(new UserStore<NokUser>(SingleToneDb.Get()));
            var identitySettings = new IdentitySettings();
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<NokUser>(manager)
            {

                AllowOnlyAlphanumericUserNames = identitySettings.AllowOnlyAlphanumericUserNames,
                RequireUniqueEmail = identitySettings.RequireUniqueEmail
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = identitySettings.MinimumPasswordLength,
                RequireNonLetterOrDigit = identitySettings.RequireNonLetterOrDigit,
                RequireDigit = identitySettings.RequireDigit,
                RequireLowercase = identitySettings.RequireLowercase,
                RequireUppercase = identitySettings.RequireUppercase,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = identitySettings.UserLockoutEnabledByDefault;
            manager.DefaultAccountLockoutTimeSpan = identitySettings.DefaultAccountLockoutTimeSpan;
            manager.MaxFailedAccessAttemptsBeforeLockout = identitySettings.MaxFailedAccessAttemptsBeforeLockout;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<NokUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<NokUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
          //  manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<NokUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
        public virtual async Task<IdentityResult> AddUserToRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<NokUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            // Add user to each role using UserRoleStore
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                await userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
            }

            // Call update once when all roles are added
            return await UpdateAsync(user).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove user from multiple roles
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="roles">list of role names</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IList<string> roles)
        {
            var userRoleStore = (IUserRoleStore<NokUser, string>)Store;

            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
            // Remove user to each role using UserRoleStore
            foreach (var role in roles.Where(userRoles.Contains))
            {
                await userRoleStore.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
            }

            // Call update once when all roles are removed
            return await UpdateAsync(user).ConfigureAwait(false);
        }

        public virtual async Task<IdentityResult> LockUserAccount(string userId, int? forDays)
        {
            var result = await SetLockoutEnabledAsync(userId, true);
            if (result.Succeeded)
            {
                if (forDays.HasValue)
                {
                    result = await SetLockoutEndDateAsync(userId, DateTimeOffset.UtcNow.AddDays(forDays.Value));
                }
                else
                {
                    result = await SetLockoutEndDateAsync(userId, DateTimeOffset.MaxValue);
                }
            }
            return result;
        }

        public virtual async Task<IdentityResult> UnlockUserAccount(string userId)
        {
            var result = await SetLockoutEnabledAsync(userId, false);
            if (result.Succeeded)
            {
                await ResetAccessFailedCountAsync(userId);
            }
            return result;
        }
        public virtual async Task<IdentityResult> ChangDisplayName(string userId,string displayName)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            user.DisplayName = displayName;
            return await UpdateAsync(user).ConfigureAwait(false);
        }
        public List<DropUserList> GetDropUserList()
        {
            return (from users in Users
                    select new DropUserList { Id = users.Id, Name = users.DisplayName }).ToList();
        }
    }

}
