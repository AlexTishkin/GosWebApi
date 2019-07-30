using GosWebApi.Models;
using System.Threading.Tasks;

namespace GosWebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task CreateRoleAsync(string role);
        Task CreateUserWithRoleAsync(string email, string password, string role);
        Task<ApplicationUser> FindByEmailAsync(string email);
    }
}