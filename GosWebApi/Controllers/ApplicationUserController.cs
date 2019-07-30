using GosWebApi.Models;
using GosWebApi.Models.Entities;
using GosWebApi.Services.Interfaces;
using GosWebApi.Services.Interfaces.Models;
using GosWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IAuthService _authService;
        private readonly ApplicationContext _db;

        public ApplicationUserController(IAuthService authService, UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            _authService = authService;
            _userManager = userManager;
            _db = context;
        }

        [HttpPost]
        [Route("/api/login")]
        //POST : /api/login
        public async Task<IActionResult> Login(LoginData model)
        {
            var token = await _authService.LoginAsync(model);
            if (token is null) return BadRequest(new { message = "Username or password is incorrect." });
            return Ok(new { token });
        }

        [HttpGet]
        [Authorize]
        [Route("/profile")]
        //POST : /api/profile
        public async Task<IActionResult> Profile()
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(t => t.Type == "UserID");
            var userId = User.Claims.First(c => c.Type == "UserID").Value;

            if (userId == null) return StatusCode(401);

            var currentUser = await _userManager.FindByIdAsync(userId);

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

            var allStatuses = await _db.Statuses.Select(s => new {s.Id, s.Name}).ToListAsync();

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