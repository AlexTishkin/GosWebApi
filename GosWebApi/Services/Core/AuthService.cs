using GosWebApi.Models;
using GosWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GosWebApi.Services.Core
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync(string role)
        {
            if (await _roleManager.FindByNameAsync(role) != null) return;
            await _roleManager.CreateAsync(new IdentityRole(role));
        }

        public async Task CreateUserWithRoleAsync(string email, string password, string role)
        {
            if (await _userManager.FindByNameAsync(email) != null) return;
            var user = new ApplicationUser {Email = email, UserName = email};
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}