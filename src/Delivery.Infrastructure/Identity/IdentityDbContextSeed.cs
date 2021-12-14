using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Identity
{
    public class IdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Shared.Identity.Constants.Roles.ADMINISTRATORS));

            var defaultUserName = "user@user.com";
            var defaultUser = new IdentityUser { UserName = defaultUserName, Email = defaultUserName };
            await userManager.CreateAsync(defaultUser, "User@123");

            string adminUserName = "admin@admin.com";
            var adminUser = new IdentityUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, "Admin@123");
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, Shared.Identity.Constants.Roles.ADMINISTRATORS);
        }
    }
}
