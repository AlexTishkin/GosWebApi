using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GosWebApi.Models;
using GosWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GosWebApi.Services.Interfaces.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GosWebApi.Services.Core
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationSettings _appSettings;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
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

        public async Task<ApplicationUser> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<string> LoginAsync(LoginData model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) return null;

            //Get role assigned to the user
            var role = await _userManager.GetRolesAsync(user);
            IdentityOptions _options = new IdentityOptions();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString()),
                    new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                }),
                //DateTime.UtcNow.AddDays(1),
                Expires = null,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterData model)
        {
            var applicationUser = new ApplicationUser {UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(applicationUser, model.Password);
            return result;
        }
    }
}