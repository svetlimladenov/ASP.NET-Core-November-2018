using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PandaToAsp.Utilities
{
    public static class Seeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            bool AdminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!AdminRoleExists)
            {
                var adminRole = new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "0" };
                var result = await roleManager.CreateAsync(adminRole);
            }

            bool UserRoleExists = await roleManager.RoleExistsAsync("User");
            if (!UserRoleExists)
            {
                var userRole = new IdentityRole() { Name = "User", NormalizedName = "USER", ConcurrencyStamp = "1" };
                var result = await roleManager.CreateAsync(userRole);
            }
        }
    }
}
