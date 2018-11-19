using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkingWithDataDemo.ModelBinder;
using WorkingWithDataDemo.ViewModels;

namespace WorkingWithDataDemo.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Create()
        {
            var model = new StudentBindingModel
            {
                Name = new FullName { FirstName = "Pesho" },
                Type = StudentType.Fake,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(
            StudentBindingModel model,

            [FromHeader]
            string connection,
            
            [FromServices]
            ILogger<StudentsController> logger,
            
            [ModelBinder(typeof(GetAllHeadersModelBinder))]
            string allMyHeaders)
        {
            logger.LogInformation(JsonConvert.SerializeObject(model));
            return Json(model);
        }
    }
}