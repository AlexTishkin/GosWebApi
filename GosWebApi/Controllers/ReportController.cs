using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
using GosWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly ApplicationContext _db;

        public ReportController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet]
        [Route("/api/themes")]
        // GET api/themes
        public async Task<IActionResult> GetThemes()
        {
            var themes = await _db.Themes.Include(t => t.SubThemes).ToListAsync();
            var themeViewModels = themes.Select(t => new ThemeViewModel(t.Id, t.Name, t.SubThemes)).ToList();
            return Json(themeViewModels);
        }

        [HttpPost]
        [Route("/api/report")]
        //POST : /api/report
        public async Task<IActionResult> AddReport(ReportViewModel report)
        {
            if (report == null || report.SubThemeId.Equals(Guid.Empty)) return BadRequest();

            var subTheme = await _db.SubThemes.FirstOrDefaultAsync(s => s.Id == report.SubThemeId);
            if (subTheme == null) return BadRequest();

            var companies = await _db.Companies
                .Include(c => c.CompanySubThemes)
                .ThenInclude(cs => cs.SubTheme)
                .ToListAsync();

            var posibleCompanies = companies
                .Where(c => c.CompanySubThemes != null &&
                            c.CompanySubThemes.Count(cs => cs.SubThemeId == report.SubThemeId) > 0)
                .ToList();

            // TODO: Выбор первой компании, реализовать логику выбора оптимальной компании
            var implementerCompanyId = posibleCompanies.First();

            var createdReport = new Report
            {
                Id = Guid.NewGuid(),
                LastName = report.LastName,
                FirstName = report.FirstName,
                MiddleName = report.MiddleName,
                Email = report.Email,
                Address = report.Address,
                Message = report.Message,
                Company = implementerCompanyId
            };

            var statusId = _db.Statuses
                .First(s => s.Name.Equals("Принят в обработку", StringComparison.OrdinalIgnoreCase)).Id;

            createdReport.ReportStatuses
                .Add(new ReportStatus {ReportId = createdReport.Id, StatusId = statusId, Datetime = DateTime.Now});

            _db.Reports.Add(createdReport);
            await _db.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        [Route("/api/reports")]
        //POST : /api/reports
        public async Task<IActionResult> GetReportsByEmail(string email)
        {
            if (email == null) return BadRequest();

            return null;
        }


        [HttpPost]
        [Route("/api/setStatus")]
        //POST : /api/setStatus
        public async Task<IActionResult> SetStatus(Guid idReport, Guid idStatus, string failMessage = null)
        {
            if (!string.IsNullOrWhiteSpace(value: failMessage))
            {
                _db.Reports.First(predicate => predicate.Id.Equals(idReport)).FailMessage = failMessage;
            }

            _db.Reports.First(predicate: predicate => predicate.Id.Equals(idReport)).ReportStatuses.Add(new ReportStatus{
                ReportId = idReport,
                StatusId = idStatus,
                Datetime = DateTime.Now
            });
            return null;
        }

    }
}