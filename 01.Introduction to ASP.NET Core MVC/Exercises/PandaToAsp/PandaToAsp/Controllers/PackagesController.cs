using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Models;
using PandaToAsp.Services;
using PandaToAsp.ViewModels.Packages;

namespace PandaToAsp.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IDbService dbService;

        public PackagesController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreatePackageViewModel();
            viewModel.Recipients = this.dbService.Db().Users.Select(u => new BaseUserViewModel()
            {
                Username = u.UserName
            }).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreatePackageInputModel model)
        {
            var recipient = this.dbService.Db().Users.Include(u => u.Packages).FirstOrDefault(u => u.NormalizedUserName == model.Recipient.ToUpper());
            var package = new Package()
            {
                RecipientId = recipient.Id,
                Description = model.Description,
                Status = PackageStatus.Pending,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
            };

            recipient.Packages.Add(package);
            this.dbService.Db().Add(package);
            this.dbService.Db().SaveChanges();
            return this.Redirect("/");
        }


        public IActionResult Delivered()
        {
            return this.View();
        }

        public IActionResult Shipped()
        {
            return this.View();
        }

        public IActionResult Pending()
        {
            return this.View();
        }

        [Authorize("Admin")]
        public IActionResult Deliver(string id)
        {
            var package = this.dbService.Db().Packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
            {
                return this.BadRequest("Invalid package");
            }
            package.Status = PackageStatus.Delivered;
            this.dbService.Db().SaveChanges();
            return this.Redirect("/Packages/Shipped");
        }

        [Authorize("Admin")]
        public IActionResult Ship(string id)
        {
            var package = this.dbService.Db().Packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
            {
                return this.BadRequest("Invalid package");
            }
            package.Status = PackageStatus.Shipped;
            var random = new Random();
            var estimateDelivery = random.Next(20, 40);
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(estimateDelivery);
            this.dbService.Db().SaveChanges();
            return this.Redirect("/Packages/Pending");
        }

        [Authorize]
        public IActionResult Acquire(string id)
        {
            var package = this.dbService.Db().Packages.Include(p => p.Recipient).FirstOrDefault(p => p.Id == id);
            if (package == null)
            {
                return this.BadRequest("Invalid package");
            }
            package.Status = PackageStatus.Acquired;
            var receipUser = this.dbService.Db().Users.FirstOrDefault(u => u.UserName == package.Recipient.UserName);
            var receipt = new Receipt()
            {
                RecipientId = package.Recipient.Id,
                IssuedOn = DateTime.UtcNow,
                PackageId = package.Id,
                Fee = (decimal)(package.Weight * 2.67),
            };
            receipUser.Receipts.Add(receipt);
            this.dbService.Db().Receipts.Add(receipt);
            this.dbService.Db().SaveChanges();
            return this.Redirect("/Receipts/Index");
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var viewModel = this.dbService.Db().Packages.Where(p => p.Id == id).Include(p => p.Recipient)
                .Select(p => new PackageDetailsViewModel()
                {
                    Status = p.Status.ToString(),
                    Address = p.ShippingAddress,
                    EstimatedDeliveryDate = p.EstimatedDeliveryDate.ToString("d"),
                    Recipient = p.Recipient.UserName,
                    Description = p.Description,
                    Weight = p.Weight
                }).FirstOrDefault();
            if (this.User.Identity.Name != viewModel.Recipient)
            {
                return BadRequest("Invalid package.");
            }
            return this.View(viewModel);
        }

    }
}