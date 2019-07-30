using GosWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GosWebApi.Controllers
{
    // Test для проверки авторизации
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthTestController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("GetUserProfile")]
        //GET : /api/UserProfile
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(new {user.Email, user.UserName});
        }

        [HttpGet]
        [Authorize(Roles = "director")]
        [Route("ForDirector")]
        //GET : /api/AuthTest/GetForDirector
        public string GetForDirector() => "Web method for Director";

        [HttpGet]
        [Authorize(Roles = "implementer")]
        [Route("ForImplementer")]
        //GET : /api/AuthTest/GetForImplementer
        public string GetForImplementer() => "Web method for Implementer";
    }
}