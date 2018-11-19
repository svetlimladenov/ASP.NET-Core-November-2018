using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;
using PandaToAsp.Services;
using PandaToAsp.ViewModels.Packages;

namespace PandaToAsp.Components
{
    [ViewComponent(Name = "PackagesTable")]
    public class PackagesTablesViewComponent : ViewComponent
    {
        private readonly IDbService dbService;

        public PackagesTablesViewComponent(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string packageStatusName)
        {
            var viewModel = new PackageTableViewModel();

            if (packageStatusName == "Delivered")
            {
                viewModel.Packages = this.dbService.Db().Packages.Where(p => p.Status.ToString() == packageStatusName || p.Status == PackageStatus.Acquired).Include(p => p.Recipient).Select(p =>
                    new BasePendingPageViewModel()
                    {
                        Description = p.Description,
                        Recipient = p.Recipient.UserName,
                        Id = p.Id,
                        ShippingAddress = p.ShippingAddress,
                        Weight = p.Weight,
                        PackageStatus = packageStatusName
                    }).ToList();
            }
            else
            {
                viewModel.Packages = this.dbService.Db().Packages.Where(p => p.Status.ToString() == packageStatusName).Include(p => p.Recipient).Select(p =>
                    new BasePendingPageViewModel()
                    {
                        Description = p.Description,
                        Recipient = p.Recipient.UserName,
                        Id = p.Id,
                        ShippingAddress = p.ShippingAddress,
                        Weight = p.Weight,
                        PackageStatus = packageStatusName
                    }).ToList();
            }

            return View(viewModel);
        }
    }
}
