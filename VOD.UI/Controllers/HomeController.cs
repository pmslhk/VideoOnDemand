using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VOD.UI.Models;
using Microsoft.AspNetCore.Identity;
using VOD.Common.Entities;
using VOD.Database.Services;

namespace VOD.UI.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<VODUser> _signInManager;

        private IDbReadService _db;
        public  HomeController(SignInManager<VODUser> signInMgr,
        IDbReadService db)
        {
            _signInManager = signInMgr;
            _db = db;
        }


        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToPage("/Account/Login", new { Area = "Identity" });
            
            return  View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
