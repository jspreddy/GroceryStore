using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using GroceryStore.Models;
using GroceryStore.StringTags;

namespace GroceryStore
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            SeedUsers(context);
            base.Seed(context);
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // create all roles.
            string[] roles = ApplicationRoles.GetAllRoles();
            foreach(string role in roles){
                if (!RoleManager.RoleExists(role)) RoleManager.Create(new IdentityRole(role));
            }

            //create admin user.
            string admin_pwd = "admin123";
            var adminUser = new ApplicationUser();
            adminUser.Email = "admin@test.com";
            adminUser.UserName = adminUser.Email;

            var adminResult = UserManager.Create(adminUser, admin_pwd);
            
            if (adminResult.Succeeded)
            {
                UserManager.AddToRole(adminUser.Id, ApplicationRoles.User.R);
                UserManager.AddToRole(adminUser.Id, ApplicationRoles.User.W);
                UserManager.AddToRole(adminUser.Id, ApplicationRoles.User.D);

                UserManager.AddToRole(adminUser.Id, ApplicationRoles.Inventory.R);
                UserManager.AddToRole(adminUser.Id, ApplicationRoles.Inventory.W);
                UserManager.AddToRole(adminUser.Id, ApplicationRoles.Inventory.D);
            }

            //create a test user
            var user = new ApplicationUser();
            user.Email = "test@test.com";
            user.UserName = user.Email;

            string user_pwd = "test123";
            var userResult = UserManager.Create(user,user_pwd);

            if (userResult.Succeeded)
            {
                UserManager.AddToRole(user.Id, ApplicationRoles.User.R);

                UserManager.AddToRole(user.Id, ApplicationRoles.Inventory.R);
                UserManager.AddToRole(user.Id, ApplicationRoles.Inventory.W);
            }
        }
    }
}
