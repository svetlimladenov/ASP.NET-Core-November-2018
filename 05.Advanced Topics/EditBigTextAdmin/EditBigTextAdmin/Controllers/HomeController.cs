using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EditBigTextAdmin.Data;
using Microsoft.AspNetCore.Mvc;
using EditBigTextAdmin.Models;
using EditBigTextAdmin.ViewModels;

namespace EditBigTextAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationdDbContext db;

        public HomeController(ApplicationdDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            ViewData["Lorem"] = db.Texts.FirstOrDefault(t => t.Name == "Lorem").Content;
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
