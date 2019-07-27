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
    public class TestController : Controller
    {
        private readonly ApplicationContext _db;

        public TestController(ApplicationContext context)
        {
            _db = context;
        }

        // GET api/test
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var themes = await _db.Themes.Include(t => t.SubThemes).ToListAsync();
            var themeViewModels = themes.Select(t => new ThemeViewModel(t.Id, t.Name, t.SubThemes));

            return Json(themeViewModels);
        }
    }
}