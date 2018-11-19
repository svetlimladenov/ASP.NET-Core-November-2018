using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChushkaToAsp.Models;
using ChushkaToAsp.Services;
using ChushkaToAsp.ViewModels.Home;
using Microsoft.AspNetCore.Http;

namespace ChushkaToAsp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDbService db;

        public HomeController(IDbService db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.View("IndexLoggedOut");
            }
            var viewModel = new IndexViewModel();
            viewModel.Products = this.db.Db().Products.Where(p => !p.IsDeleted).Select(p => new BaseProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();

            return this.View(viewModel);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
