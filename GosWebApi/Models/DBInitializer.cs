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
        public static async Task InitializeAsync(
            ApplicationContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            #region Database fill

            // TODO: Тупая перезачистка


            // TODO: Тупая перезачистка
            context.Roles.RemoveRange(context.Roles.ToList());
            context.Users.RemoveRange(context.Users.ToList());
            context.SubThemes.RemoveRange(context.SubThemes.ToList());
            context.Themes.RemoveRange(context.Themes.ToList());
            context.Companies.RemoveRange(context.Companies.ToList());

            await InitializeRoles(roleManager);
            await InitializeUsers(userManager);

            var theme1 = new Theme {Name = "Тема 1"};
            var theme2 = new Theme {Name = "Тема 2"};
            context.Themes.Add(theme1);
            context.Themes.Add(theme2);
            context.SaveChanges();

            var subTheme11 = new SubTheme {Id = Guid.NewGuid(), Name = "1 Подтема 1", Theme = theme1};
            var subTheme12 = new SubTheme {Id = Guid.NewGuid(), Name = "1 Подтема 2", Theme = theme1};

            var subTheme21 = new SubTheme {Id = Guid.NewGuid(), Name = "2 Подтема 1", Theme = theme2};
            var subTheme22 = new SubTheme {Id = Guid.NewGuid(), Name = "2 Подтема 2", Theme = theme2};
            context.SubThemes.AddRange(new List<SubTheme> {subTheme11, subTheme12, subTheme21, subTheme22});
            context.SaveChanges();

            // Add Company and Subthemes 
            var c1 = new Company {Id = Guid.NewGuid(), Name = "Company1"};
            var c2 = new Company {Id = Guid.NewGuid(), Name = "Company2"};

            context.SaveChanges();

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme11.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme12.Id});

            c2.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c2.Id, SubThemeId = subTheme21.Id});
            c2.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c2.Id, SubThemeId = subTheme22.Id});

            context.Companies.AddRange(new List<Company> {c1, c2});

            context.SaveChanges();

            #endregion
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
                ApplicationUser implementer = new ApplicationUser
                    {Email = implementerEmail, UserName = implementerEmail};
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