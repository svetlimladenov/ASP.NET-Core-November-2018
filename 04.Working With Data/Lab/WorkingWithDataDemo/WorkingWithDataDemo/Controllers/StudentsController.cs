using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkingWithDataDemo.ModelBinder;
using WorkingWithDataDemo.ViewModels;

namespace WorkingWithDataDemo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IHostingEnvironment environment;

        public StudentsController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

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
        public async Task<IActionResult> Create(
            StudentBindingModel model,

            [FromHeader]
            string connection,
            
            [FromServices]
            ILogger<StudentsController> logger,
            
            [ModelBinder(typeof(GetAllHeadersModelBinder))]
            string allMyHeaders)
        {
            if (this.ModelState.IsValid)
            {
                // TODO: Persist
                var fileName = this.environment.WebRootPath + "/files/" + "1.jpg";
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                return this.Json(model);
            }
            else
            {
                return this.View(model);
            }
        }
    }
}