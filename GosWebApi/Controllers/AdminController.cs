﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
using GosWebApi.Models.Entities;
using GosWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly ApplicationContext _db;

        private UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        [Route("/api/implementorReports")]
        //POST : /api/implementorReports
        public async Task<IActionResult> GetImplementorReports()
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(t => t.Type == "UserID");
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            //if (userId == null) return StatusCode(401);

            var currentUser = await _userManager.FindByIdAsync(userId);

            var reports = _db.Reports
                .Include(r => r.Company)
                .Include(r => r.ReportStatuses)
                .ThenInclude(r => r.Status)
                .Where(r => r.CompanyId == currentUser.CompanyId)
                .Select(r => new ImplementorViewModel(r.Id, r.LastName, r.FirstName, r.MiddleName, r.Message,  r.FailMessage,  GetStartDate(r), GetStatuses(r)))
                .ToList();

            return Json(reports);
        }

        private DateTime GetStartDate(Report report)
        {
            var r = report.ReportStatuses.Min(rs => rs.Datetime);
            return r;
        }

        private IEnumerable<StatusViewModel> GetStatuses(Report report)
        {
            var statuses = report.ReportStatuses.Select(rs => new StatusViewModel
                { Id = rs.StatusId, Datetime = rs.Datetime, Name = rs.Status.Name, Order = rs.Status.Order });
            return statuses;
        }

    }
}