using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Models
{
    public class DBInitializer
    {
        public static async Task InitializeAsync(
            ApplicationContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            #region Database fill

            // перезачистка
            db.Roles.RemoveRange(db.Roles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.SubThemes.RemoveRange(db.SubThemes.ToList());
            db.Themes.RemoveRange(db.Themes.ToList());
            db.Companies.RemoveRange(db.Companies.ToList());
            db.Reports.RemoveRange(db.Reports.Include(r=>r.ReportStatuses).ToList());
            db.Statuses.RemoveRange(db.Statuses.ToList());
            db.Regions.RemoveRange(db.Regions.ToList());
            db.Companies.RemoveRange(db.Companies.ToList());

            await InitializeRoles(roleManager);
            await InitializeUsers(userManager);

            var theme1 = new Theme {Name = "ЖКХ"};
            var theme2 = new Theme {Name = "Автомобильные дороги"};


            var theme3 = new Theme {Name = "Связь и телекоммуникации"};
            var theme4 = new Theme {Name = "Трудовые отношения"};
            var theme5 = new Theme {Name = "Социальная сфера"};
            var theme6 = new Theme {Name = "Экология"};
            var theme7 = new Theme {Name = "Торговля, товары и услуги"};



            db.Themes.AddRange(new List<Theme>{ theme1, theme2, theme3, theme4, theme5, theme6, theme7});
            //db.Themes.Add(theme2);
            db.SaveChanges();

            var subTheme11 = new SubTheme {Id = Guid.NewGuid(), Name = "Многоквартирные дома", Theme = theme1};
            var subTheme12 = new SubTheme {Id = Guid.NewGuid(), Name = "Частный сектор", Theme = theme1};

            var subTheme21 = new SubTheme  {Id = Guid.NewGuid(), Name = "Безопасная дорога в учебное заведение", Theme = theme2};
            var subTheme22 = new SubTheme {Id = Guid.NewGuid(), Name = "Дорожная разметка", Theme = theme2};
            /////////// subThemes
            var subTheme31 = new SubTheme  {Id = Guid.NewGuid(), Name = "Отсутствие услуги доступа в сеть Интернет", Theme = theme3};
            var subTheme32 = new SubTheme {Id = Guid.NewGuid(), Name = "Нарушение графика работы отделений связи", Theme = theme3};
            var subTheme33 = new SubTheme {Id = Guid.NewGuid(), Name = "Некачественное предоставление услуг кабельного и цифрового телевидения", Theme = theme3};

            var subTheme41 = new SubTheme  {Id = Guid.NewGuid(), Name = "Не оформленные трудовые отношения", Theme = theme4};
            var subTheme42 = new SubTheme {Id = Guid.NewGuid(), Name = "Несвоевременная выплата заработной платы", Theme = theme4};

            var subTheme51 = new SubTheme  {Id = Guid.NewGuid(), Name = "Наличие очереди на места в дошкольные учреждения", Theme = theme5};
            var subTheme52 = new SubTheme {Id = Guid.NewGuid(), Name = "Нарушения в вопросах опеки и попечительства", Theme = theme5};

            var subTheme61 = new SubTheme  {Id = Guid.NewGuid(), Name = "Загрязнение водных объектов", Theme = theme6};
            var subTheme62 = new SubTheme {Id = Guid.NewGuid(), Name = "Загрязнение атмосферы пылевыми выбросами", Theme = theme6};

            var subTheme71 = new SubTheme  {Id = Guid.NewGuid(), Name = "Нарушения, связанные с предоставлением услуг ОСАГО", Theme = theme7};
            var subTheme72 = new SubTheme {Id = Guid.NewGuid(), Name = "Неудовлетворительное качество товара, оказания услуги", Theme = theme7};




            db.SubThemes.AddRange(new List<SubTheme> {subTheme11, subTheme12
                , subTheme21, subTheme22
                , subTheme31, subTheme32, subTheme33
                , subTheme41, subTheme42
                , subTheme51, subTheme52
                , subTheme61, subTheme62
                , subTheme71, subTheme72

            });
            db.SaveChanges();

            // Add Company and Subthemes 
            var c1 = new Company {Id = Guid.NewGuid(), Name = "ООО \"Freedom\""};
            var c2 = new Company {Id = Guid.NewGuid(), Name = "ОАО \"Uneal\""};

            db.SaveChanges();

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme11.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme12.Id});

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme21.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme22.Id});

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme31.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme32.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme33.Id});

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme41.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme42.Id});

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme51.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme52.Id});

            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme61.Id});
            c1.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme62.Id});

            c2.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme71.Id});
            c2.CompanySubThemes.Add(new CompanySubTheme {CompanyId = c1.Id, SubThemeId = subTheme72.Id});


            db.Companies.AddRange(new List<Company> {c1, c2});

            db.SaveChanges();


            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Отклонен",
                Order = 5
            });
            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Ожидание оплаты",
                Order = 1
            });
            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Принят в обработку",
                Order = 0
            });
            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Исполнение",
                Order = 2
            });
            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Проверка",
                Order = 3
            });
            db.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                Name = "Завершен",
                Order = 4
            });
            db.SaveChanges();

            var r1 = new Region
            {
                Id = Guid.Parse("5B5317B8-1CB3-4FBB-877D-6BB66A9E60CB"),
                Name = "Центральный район"
            };
            var r2 = new Region
            {
                Id = Guid.Parse("95536638-6086-4E08-884A-70D4B0C698FD"),
                Name = "Сеймский район"
            };

            db.Regions.Add(r1);
            db.Regions.Add(r2);
            db.SaveChanges();

            // Beda
            //Report report = new Report
            //{
            //    Id = Guid.Parse("CE11234F-D975-4AD4-A07F-B3175045D4E5"),
            //    LastName = "Клюев",
            //    FirstName = "Антон",
            //    MiddleName = "Александрович",
            //    Message = "\"Замороженная\" стройка",
            //    FailMessage = "Стройку так и не разморозили...",
            //    Region = r1,
            //    Address = "г. Курск, пр-т Победы, дом 32, кв. 192",
            //    Email = "freemandns@mail.ru",
            //    CompanyId = c1.Id,
            //    Mark = 1,
            //    SubTheme = subTheme11,
            //    MarkDescription = "Работа не выполнена"
            //};

            //var report2 = new Report
            //{
            //    Id = Guid.Parse("DCA26404-32BC-473B-8D45-AEBDE68535C9"),
            //    LastName = "Николенко",
            //    FirstName = "Анна",
            //    MiddleName = "Петровна",
            //    Message = "Безопасная дорога в школу на дорогах в границах городских округов и сельских поселений",
            //    FailMessage = string.Empty,
            //    Region = r2,
            //    Address = "г. Курск, пр-т Победы, дом 32, кв. 192",
            //    Email = "freemandns@mail.ru",
            //    CompanyId = c2.Id,
            //    Mark = 1,
            //    SubTheme = subTheme21,
            //    MarkDescription = "Работа не выполнена"
            //};

            //db.Reports.Add(report);
            //db.Reports.Add(report2);
            //db.SaveChanges();

            //report.ReportStatuses.Add(new ReportStatus
            //{
            //    ReportId = Guid.Parse("CE11234F-D975-4AD4-A07F-B3175045D4E5"),
            //    Status = db.Statuses.First(),
            //    Datetime = DateTime.Now
            //});
            
            //report.ReportStatuses.Add(new ReportStatus
            //{
            //    ReportId = Guid.Parse("DCA26404-32BC-473B-8D45-AEBDE68535C9"),
            //    Status = db.Statuses.Last(),
            //    Datetime = DateTime.Now
            //});
            //db.SaveChanges();


            // add users to company
            var _impl1 = await userManager.FindByEmailAsync("impl@impl.ru");
            var _dir1 = await userManager.FindByEmailAsync("director@director.ru");

            _impl1.Company = c1;
            _dir1.Company = c1;
            db.SaveChanges();
            // add users to company 2
            var _impl2 = await userManager.FindByEmailAsync("impl2@impl.ru");
            var _dir2 = await userManager.FindByEmailAsync("director2@director.ru");

            _impl2.Company = c2;
            _dir2.Company = c2;
            db.SaveChanges();

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

            var implementer2Email = "impl2@impl.ru";
            var implementer2Password = "123456";

            var director2Email = "director2@director.ru";
            var director2Password = "123456";

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

            // company 2
            if (await userManager.FindByNameAsync(implementer2Email) == null)
            {
                ApplicationUser implementer2 = new ApplicationUser {Email = implementer2Email, UserName = implementer2Email};
                IdentityResult result = await userManager.CreateAsync(implementer2, implementer2Password);
                if (result.Succeeded) await userManager.AddToRoleAsync(implementer2, ApplicationRoles.IMPLEMENTER);
            }

            if (await userManager.FindByNameAsync(director2Email) == null)
            {
                ApplicationUser director2 = new ApplicationUser {Email = director2Email, UserName = director2Email};
                IdentityResult result = await userManager.CreateAsync(director2, director2Password);
                if (result.Succeeded) await userManager.AddToRoleAsync(director2, ApplicationRoles.DIRECTOR);
            }
        }
    }
}