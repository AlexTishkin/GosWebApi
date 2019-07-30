using GosWebApi.Models;
using System.Threading.Tasks;
using GosWebApi.Services.Interfaces.Models;
using Microsoft.AspNetCore.Identity;

namespace GosWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task CreateRoleAsync(string role);

        Task CreateUserWithRoleAsync(string email, string password, string role);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<IdentityResult> RegisterAsync(RegisterData model);

        Task<string> LoginAsync(LoginData model);
    }
}