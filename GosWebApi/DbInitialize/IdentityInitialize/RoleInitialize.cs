using GosWebApi.Services.Interfaces;
using GosWebApi.Services.Interfaces.Models;
using System.Threading.Tasks;

namespace GosWebApi.DbInitialize.IdentityInitialize
{
    public class RoleInitialize
    {
        public static async Task InitializeAsync(IAuthService authService)
        {
            await authService.CreateRoleAsync(ApplicationRole.DIRECTOR);
            await authService.CreateRoleAsync(ApplicationRole.IMPLEMENTER);
        }
    }
}