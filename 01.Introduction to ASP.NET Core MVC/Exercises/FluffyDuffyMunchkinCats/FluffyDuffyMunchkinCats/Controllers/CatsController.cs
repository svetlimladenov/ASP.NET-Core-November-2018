using System.Linq;
using FluffyDuffyMunchkinCats.Data;
using FluffyDuffyMunchkinCats.Models.DbModels;
using FluffyDuffyMunchkinCats.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FluffyDuffyMunchkinCats.Controllers
{
    public class CatsController : Controller
    {
        private ApplicationDbContext db;

        public CatsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Cat model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            this.db.Cats.Add(model);
            this.db.SaveChanges();
            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.db.Cats.Select(c => new CatDetailsViewModel
            {
                Id = c.Id,
                Age = c.Age,
                ImageUrl = c.ImageUrl,
                Name = c.Name,
                Breed = c.Breed
            }).FirstOrDefault(c => c.Id == id);

            return this.View(viewModel);
        }
    }
}