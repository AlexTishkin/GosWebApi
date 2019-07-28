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
                SubTheme = subTheme,
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


        [HttpGet]
        [Route("/api/reports/{email}")]
        //POST : /api/reports
        public async Task<IActionResult> GetReportsByEmail(string email)
        {
            if (email == null) return BadRequest();

            var reports = await _db.Reports
                .Include(r => r.SubTheme)
                .Include(r => r.Company)
                .Include(r => r.ReportStatuses)
                .ThenInclude(rs => rs.Status)
                .Where(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            var outputReports = reports.Select(r =>
                    new ReportArrayItemViewModel(r.Company.Name
                        , GetThemeName(r)
                        , r.SubTheme.Name
                        // ТУТ КОСЯК!!! ИСПРАВИТЬ В БАЗЕ
                        , DateTime.Now
                        , GetStatuses(r)))
                .ToList();

            return Json(outputReports);
        }

        #region Additional functions to /api/reports

        private string GetThemeName(Report report)
        {
            var subThemeId = report.SubThemeId;
            var themeName = _db.SubThemes
                .Include(s => s.Theme)
                .First(s => s.Id == subThemeId)
                .Theme.Name;
            return themeName;
        }

        private DateTime GetStartDate(Report report)
        {
            var r = report.ReportStatuses.Min(rs => rs.Datetime);
            return r;
        }

        private IEnumerable<StatusViewModel> GetStatuses(Report report)
        {
            var statuses = report.ReportStatuses.Select(rs => new StatusViewModel
                {Id = rs.StatusId, Datetime = rs.Datetime, Name = rs.Status.Name});
            return statuses;
        }

        #endregion

        [HttpPost]
        [Route("/api/setStatus")]
        //POST : /api/setStatus
        public async Task<IActionResult> SetStatus(SetStatusViewModel setStatusViewModel)
        {
            if (setStatusViewModel.ReportId == null || setStatusViewModel.StatusId == null) return BadRequest();

            if (!string.IsNullOrWhiteSpace(value: setStatusViewModel.FailMessage))
            {
                var changedReport =
                    await _db.Reports.FirstAsync(predicate: predicate =>
                        predicate.Id.Equals(setStatusViewModel.ReportId));
                changedReport.FailMessage = setStatusViewModel.FailMessage;
                _db.Entry(changedReport).State = EntityState.Modified;
            }

            var addedReport = _db.Reports.First(predicate => predicate.Id.Equals(setStatusViewModel.ReportId));
            addedReport.ReportStatuses.Add(
                new ReportStatus
                {
                    ReportId = setStatusViewModel.ReportId.Value, StatusId = setStatusViewModel.StatusId.Value,
                    Datetime = DateTime.Now
                });

            _db.Entry(addedReport).State = EntityState.Modified;

            _db.SaveChanges();
            return Ok();
        }
    }
}