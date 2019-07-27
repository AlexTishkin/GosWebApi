﻿using GosWebApi.Models;
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
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.Email,
                user.UserName
            };
        }

        [HttpGet]
        [Authorize(Roles = "director")]
        [Route("ForDirector")]
        public string GetForDirector()
        {
            return "Web method for Director";
        }

        [HttpGet]
        [Authorize(Roles = "implementer")]
        [Route("ForImplementer")]
        public string GetImplementer()
        {
            return "Web method for Implementer";
        }
    }
}