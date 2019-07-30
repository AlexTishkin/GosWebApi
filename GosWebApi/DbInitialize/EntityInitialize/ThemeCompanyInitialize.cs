using GosWebApi.Models;
using System;
using System.Threading.Tasks;
using GosWebApi.Models.Entities;
using GosWebApi.Services.Interfaces;

namespace GosWebApi.DbInitialize.EntityInitialize
{
    public class ThemeCompanyInitialize
    {
        public static async Task InitializeAsync(ApplicationContext db, IAuthService authService)
        {
            #region Initialize Themes ans SubThemes

            var theme1 = new Theme("ЖКХ");
            var theme2 = new Theme("Автомобильные дороги");
            var theme3 = new Theme("Связь и телекоммуникации");

            db.Themes.AddRange(theme1, theme2, theme3);
            db.SaveChanges();

            var subTheme11 = new SubTheme(Guid.NewGuid(), "Многоквартирные дома", theme1);
            var subTheme12 = new SubTheme(Guid.NewGuid(), "Частный сектор", theme1);

            var subTheme21 = new SubTheme(Guid.NewGuid(), "Безопасная дорога в учебное заведение", theme2);
            var subTheme22 = new SubTheme(Guid.NewGuid(), "Дорожная разметка", theme2);

            var subTheme31 = new SubTheme(Guid.NewGuid(), "Отсутствие услуги доступа в сеть Интернет", theme3);
            var subTheme32 = new SubTheme(Guid.NewGuid(), "Нарушение графика работы отделений связи", theme3);
            var subTheme33 = new SubTheme(Guid.NewGuid(),
                "Некачественное предоставление услуг кабельного и цифрового телевидения", theme3);

            db.SubThemes.AddRange(subTheme11, subTheme12, subTheme21, subTheme22, subTheme31, subTheme32, subTheme33);
            db.SaveChanges();

            #endregion

            #region Initialize Companies and bind them with subthemes

            var c1 = new Company(Guid.NewGuid(), "ООО \"ЖКХ и Авто\"");
            var c2 = new Company(Guid.NewGuid(), "ОАО \"Интернет\"");

            c1.AddSubThemes(subTheme11, subTheme12, subTheme21, subTheme22);
            c2.AddSubThemes(subTheme31, subTheme32, subTheme33);

            db.Companies.AddRange(c1, c2);
            db.SaveChanges();

            #endregion

            #region Bind companies with users (Badly: Users in UserInitialize)

            #region Company 1

            var impl1 = await authService.FindByEmailAsync("impl@impl.ru");
            impl1.Company = c1;

            var dir1 = await authService.FindByEmailAsync("director@director.ru");
            dir1.Company = c1;

            #endregion

            #region Company 2

            var impl2 = await authService.FindByEmailAsync("impl2@impl.ru");
            impl2.Company = c2;

            var dir2 = await authService.FindByEmailAsync("director2@director.ru");
            dir2.Company = c2;

            #endregion

            db.SaveChanges();

            #endregion
        }
    }
}