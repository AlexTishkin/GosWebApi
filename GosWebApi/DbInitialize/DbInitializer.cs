using GosWebApi.DbInitialize.EntityInitialize;
using GosWebApi.DbInitialize.IdentityInitialize;
using GosWebApi.Models;
using GosWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace GosWebApi.DbInitialize
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationContext db, IAuthService authService)
        {
            await RoleInitialize.InitializeAsync(authService);
            await UserInitialize.InitializeAsync(authService);
            await StatusInitialize.InitializeAsync(db);
            await RegionInitialize.InitializeAsync(db);
            await ThemeCompanyInitialize.InitializeAsync(db, authService);
        }
    }
}