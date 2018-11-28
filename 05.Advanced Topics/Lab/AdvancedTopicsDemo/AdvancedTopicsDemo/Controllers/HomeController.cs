using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdvancedTopicsDemo.Models;
using Microsoft.AspNetCore.Http;
using AdvancedTopicsDemo.Extensions;
namespace AdvancedTopicsDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            this.HttpContext.Session.Set<ErrorViewModel>("anonymous",new ErrorViewModel{ RequestId = "shit"});
            this.HttpContext.Session.SetString("VisitedHomePage", "sadfasdfasdfaf");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            
            var errorViewModel = this.HttpContext.Session.Get<ErrorViewModel>("anonymous");
            ViewData["Message"] = errorViewModel.RequestId;

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
