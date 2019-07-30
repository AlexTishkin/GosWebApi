using System.Collections.Generic;
using GosWebApi.Services.Interfaces;
using GosWebApi.Services.Interfaces.Models;
using System.Threading.Tasks;

namespace GosWebApi.DbInitialize.IdentityInitialize
{
    public class UserInitialize
    {
        public static async Task InitializeAsync(IAuthService authService)
        {
            // TODO: Read from outer data source
            var users = new Dictionary<string, (string, string)>
            {
                {"impl@impl.ru", ("123456", ApplicationRole.IMPLEMENTER)},
                {"impl2@impl.ru", ("123456", ApplicationRole.IMPLEMENTER)},
                {"director@director.ru", ("123456", ApplicationRole.DIRECTOR)},
                {"director2@director.ru", ("123456", ApplicationRole.DIRECTOR)},
            };

            foreach (var user in users)
            {
                await authService.CreateUserWithRoleAsync(user.Key, user.Value.Item1, user.Value.Item2);
            }

        }
    }
}