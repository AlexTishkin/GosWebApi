using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.Models
{
    public class DBInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await InitializeRoles(roleManager);
            await InitializeUsers(userManager);
        }

        private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(ApplicationRoles.DIRECTOR) == null)
                await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.DIRECTOR));

            if (await roleManager.FindByNameAsync(ApplicationRoles.IMPLEMENTER) == null)
                await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.IMPLEMENTER));
        }

        private static async Task InitializeUsers(UserManager<ApplicationUser> userManager)
        {
            var implementerEmail = "impl@impl.ru";
            var implementerPassword = "123456";

            var directorEmail = "director@director.ru";
            var directorPassword = "123456";

            if (await userManager.FindByNameAsync(implementerEmail) == null)
            {
                ApplicationUser implementer = new ApplicationUser {Email = implementerEmail, UserName = implementerEmail};
                IdentityResult result = await userManager.CreateAsync(implementer, implementerPassword);
                if (result.Succeeded) await userManager.AddToRoleAsync(implementer, ApplicationRoles.IMPLEMENTER);
            }

            if (await userManager.FindByNameAsync(directorEmail) == null)
            {
                ApplicationUser director = new ApplicationUser {Email = directorEmail, UserName = directorEmail};
                IdentityResult result = await userManager.CreateAsync(director, directorPassword);
                if (result.Succeeded) await userManager.AddToRoleAsync(director, ApplicationRoles.DIRECTOR);
            }
        }
    }
}