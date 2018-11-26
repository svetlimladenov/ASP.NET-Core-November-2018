using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EditBigTextAdmin.Data;
using EditBigTextAdmin.Models;
using EditBigTextAdmin.ViewModels.Text;
using Microsoft.AspNetCore.Mvc;

namespace EditBigTextAdmin.Controllers
{
    public class TextController : Controller
    {
        private readonly ApplicationdDbContext db;

        public TextController(ApplicationdDbContext db)
        {
            this.db = db;
        }

        public IActionResult Edit(string name)
        {
            if (db.Texts.FirstOrDefault(x => x.Name == name) == null)
            {
                db.Texts.Add(new Text() {Content = "", Name = name, Styles = ""});
            }

            db.SaveChanges();

            var viewModel = db.Texts.Select(x => new EditTextBindingModel()
            {
                Id = x.Id,
                Content = x.Content,
                Name = x.Name,

            }).FirstOrDefault(x => x.Name == name);

            
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditTextBindingModel model)
        {
            var textId = model.Id;
            var textToEdit = db.Texts.FirstOrDefault(x => x.Id == textId);
            if (textToEdit != null) textToEdit.Content = model.Content;
            db.SaveChanges();
            return Redirect("/");
        }
    }
}
