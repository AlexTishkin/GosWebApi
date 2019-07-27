using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
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
            //var themes = await _db.Themes.Include(t => t.SubThemes).ToListAsync();
            //var themeViewModels = themes.Select(t => new ThemeViewModel(t.Id, t.Name, t.SubThemes));

            return Ok();
        }
    }
}