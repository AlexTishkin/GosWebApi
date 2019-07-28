using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GosWebApi.Models;
using GosWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly ApplicationContext _db;

        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings,
            ApplicationContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _db = context;
        }

        //[HttpPost]
        //[Route("Register")]
        ////POST : /api/ApplicationUser/Register
        //public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        //{
        //    var applicationUser = new ApplicationUser()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FullName = model.FullName
        //    };

        //    try
        //    {
        //        var result = await _userManager.CreateAsync(applicationUser, model.Password);
        //        await _userManager.AddToRoleAsync(applicationUser, model.Role);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        [Route("/login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
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
                    Expires = null, //DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new {token});
            }
            else return BadRequest(new {message = "Username or password is incorrect."});
        }

        [HttpGet]
        [Authorize]
        [Route("/profile")]
        //POST : /api/profile
        public async Task<IActionResult> Profile()
        {
            // TODO: Can be mistakes...
            var userId = HttpContext.User.Claims.FirstOrDefault(t => t.Type == "UserID");
            if (userId == null) return StatusCode(401);

            var currentUser = await _userManager.FindByIdAsync(userId.Value);

            var company = _db.Companies.First(c => c.Id == currentUser.CompanyId);

            var reports = _db.Reports
                .Include(r => r.ReportStatuses)
                .ThenInclude(rs => rs.Status)
                .Where(r => r.CompanyId == company.Id);

            var profileReports = reports.Select(r =>
                new ProfileReportViewModel(r.Id, r.Message, GetStartDate(r), GetLastStatus(r))).ToList();

            var profileViewModel = new ProfileViewModel(currentUser.LastName
                , currentUser.FirstName
                , currentUser.MiddleName
                , profileReports);

            var allStatuses = await _db.Statuses.Select(s => new Ref(s.Id, s.Name)).ToListAsync();

            return Json(new
            {
                ProfileViewModel = profileViewModel,
                AllStatuses = allStatuses
            });
        }

        private DateTime GetStartDate(Report report) => report.ReportStatuses.Min(rs => rs.Datetime);

        private StatusViewModel GetLastStatus(Report report)
        {
            var dateTime = report.ReportStatuses.Max(rs => rs.Datetime);
            var status = report.ReportStatuses.First(rs => rs.Datetime == dateTime).Status;
            return new StatusViewModel {Id = status.Id, Name = status.Name, Datetime = dateTime};
        }
        
    }
}