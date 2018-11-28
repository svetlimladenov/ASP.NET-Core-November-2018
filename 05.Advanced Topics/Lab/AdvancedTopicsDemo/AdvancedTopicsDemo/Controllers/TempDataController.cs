using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTopicsDemo.Controllers
{
    public class TempDataController : Controller
    {
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form([Required]string data)
        {
            this.TempData["ThankYouMessage"] = $"Thank you for submitting {data}";
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            this.ViewData["ThankYouMessage"] = this.TempData["ThankYouMessage"];
            return this.View();
        }
    }
}