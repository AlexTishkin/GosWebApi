using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
using GosWebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public HomeController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        { 
        }

        //public ActionResult Index()
        //{
        //    return this.Html("~/Index.html");
        //}
    }
}