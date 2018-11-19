using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using PandaToAsp.Services;
using PandaToAsp.ViewModels;
using PandaWebApp.Controllers;
using PandaWebApp.ViewModels.Home;

namespace PandaToAsp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDbService dbService;
        public HomeController(IDbService dbContext)
        {
            dbService = dbContext;
        }


        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("IndexLoggedOut");
            }

            var user = dbService.Db().Users.Include(u => u.Packages).FirstOrDefault(x => x.NormalizedUserName == this.User.Identity.Name.ToUpper());

            var pending = user.Packages.Where(p => p.Status == PackageStatus.Pending).Select(p => new BasePackageViewModel()
            {
                Description = p.Description,
                Id = p.Id,
            });

            var shipped = user.Packages.Where(p => p.Status == PackageStatus.Shipped).Select(p => new BasePackageViewModel()
            {
                Description = p.Description,
                Id = p.Id,
            });

            var delivered = user.Packages.Where(p => p.Status == PackageStatus.Delivered).Select(p => new BasePackageViewModel()
            {
                Description = p.Description,
                Id = p.Id,
            });

            var viewModel = new IndexViewModel();
            viewModel.Pending = pending.ToList();
            viewModel.Shipped = shipped.ToList();
            viewModel.Delivered = delivered.ToList();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
