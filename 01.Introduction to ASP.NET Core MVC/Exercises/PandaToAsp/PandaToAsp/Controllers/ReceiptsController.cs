using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaToAsp.Services;
using PandaWebApp.Controllers;
using PandaWebApp.ViewModels.Receipts;

namespace PandaToAsp.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IDbService dbService;

        public ReceiptsController(IDbService dbService)
        {
            this.dbService = dbService;
        }
        public IActionResult Index()
        {
            var viewModel = new ReceiptIndexViewModel();
            viewModel.Receipts = this.dbService.Db().Receipts.Include(r => r.Recipient)
                .Where(r => r.Recipient.NormalizedUserName == this.User.Identity.Name.ToUpper())
                .Select(r => new BaseReceiptIndexViewModel()
                {
                    Recipient = r.Recipient.UserName,
                    Fee = r.Fee,
                    Id = r.Id,
                    IssuedOn = r.IssuedOn.ToString("d"),
                }).ToList();

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            //TODO SECURE
            var receipt =
                this.dbService.Db().Receipts.Include(r => r.Recipient).FirstOrDefault(r => r.Id == id && r.Recipient.NormalizedUserName == this.User.Identity.Name.ToUpper());

            if (receipt == null)
            {
                return BadRequest("Invalid receipt.");
            }

            var package = this.dbService.Db().Packages.FirstOrDefault(x => x.Id == receipt.PackageId);
            var viewModel = new ReceiptDetailsViewModel()
            {
                DeliveryAddress = package.ShippingAddress,
                Description = package.Description,
                Recipient = receipt.Recipient.UserName,
                Id = receipt.Id,
                Weight = package.Weight,
                IssuedOn = receipt.IssuedOn.ToString("d"),
                Fee = receipt.Fee
            };
            return this.View(viewModel);
        }
    }
}