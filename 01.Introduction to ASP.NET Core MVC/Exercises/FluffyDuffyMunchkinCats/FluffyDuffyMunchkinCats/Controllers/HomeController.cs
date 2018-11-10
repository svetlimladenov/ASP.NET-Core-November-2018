using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluffyDuffyMunchkinCats.Data;
using Microsoft.AspNetCore.Mvc;
using FluffyDuffyMunchkinCats.Models;
using FluffyDuffyMunchkinCats.Models.ViewModels;

namespace FluffyDuffyMunchkinCats.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                Cats = this.db.Cats.ToArray()
            };
            return View(viewModel);
        } 
    }
}
