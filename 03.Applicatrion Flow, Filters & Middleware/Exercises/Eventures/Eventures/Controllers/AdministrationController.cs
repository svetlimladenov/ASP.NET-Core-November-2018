using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Services.AdministrationsServices;
using Eventures.ViewModels.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IAdministrationsService administrationsService;

        public AdministrationController(IAdministrationsService administrationsService)
        {
            this.administrationsService = administrationsService;
        }

        public IActionResult Index()
        {
            var viewModel = new PromoteDemoteViewModel();
            viewModel.AdminUsers = administrationsService.GetAdminUsers();
            viewModel.NonAdminUsers = administrationsService.GetNonAdminUsers();

            return View(viewModel);
        }

        public IActionResult Promote(string id)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Demote(string id)
        {
            return RedirectToAction("Index");
        }
    }
}