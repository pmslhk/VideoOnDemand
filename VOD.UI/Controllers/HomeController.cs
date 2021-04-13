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

        //10장
        private readonly IUIReadService _db;
        public HomeController(SignInManager<VODUser> signInMgr, IUIReadService db)
        {
            _signInManager = signInMgr;
            _db = db;
        }




        //9장
        //private IDbReadService _db;
        //public HomeController(SignInManager<VODUser> signInMgr, IDbReadService db)
        //{
        //    _signInManager = signInMgr;
        //    _db = db;

        //}

        //9장
       // public IActionResult Index()
        public async Task<IActionResult> Index()
        {
            //9장
            //var result1 = await _db.SingleAsync<Download>(d => d.Id.Equals(3));
            //var courses = await _db.GetCourses("dea61c61-773c-43e3-887d-7464e8cc2929");

            var course = await _db.GetCourse("dea61c61-773c-43e3-887d-7464e8cc2929", 1);


            //var video = await _db.GetVideo("d0200875-0f47-4975-a0fb-4d8df954ec79", 1);


            if (!_signInManager.IsSignedIn(User)) return RedirectToPage("/Account/Login", new { Area = "Identity" });
            
            return View();
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
