using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcIntro.Data;
using MvcIntro.Models;
using MvcIntro.Services;

namespace MvcIntro.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGreetingService configuration;
        private readonly IUsersService usersService;

        public HomeController(
            IGreetingService configuration,
            IUsersService usersService)
        {
            this.configuration = configuration;
            this.usersService = usersService;
        }
        public IActionResult Index()
        {
            var greeting = this.configuration.GetGreeting();
            var userCount = this.usersService.Count();
            ViewData["UserCount"] = userCount;
            ViewBag.Name = "Kalina";
            ViewData["HI"] = this.configuration.GetGreeting() + ", " + this.User.Identity.Name;
            var model = new IndexViewModel {Items = new []{"op", "p[", "zaebi"}};
            return View(model);
        }

        public IActionResult GetByUsername(string username)
        {
            return this.Content(username);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();  
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
