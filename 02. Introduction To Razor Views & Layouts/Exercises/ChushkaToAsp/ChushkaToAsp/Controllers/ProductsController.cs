using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChushkaToAsp.Services;
using ChushkaToAsp.ViewModels.Products;
using ChushkaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChushkaToAsp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDbService dbService;

        public ProductsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [Authorize("Admin")]
        public IActionResult Create()
        {         
            return View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Create(ProductBindingModel model)
        {
            Enum.TryParse(model.Type, out ProductType type);
            var product = new Product()
            {
                Price = model.Price,
                Description = model.Description,
                Name = model.Name,
                Type = type,
            };

            this.dbService.Db().Products.Add(product);
            this.dbService.Db().SaveChanges();

            return this.Redirect("/Products/Details?id=" + product.Id);
        }

        [Authorize("Admin")]
        public IActionResult Edit(string id)
        {
            var viewModel = new ProductBindingModel()
            {
                Id = id,
            };
            return this.View(viewModel);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Edit(ProductBindingModel model)
        {
            var currentProduct = this.dbService.Db().Products.FirstOrDefault(p => p.Id == model.Id);
            if (currentProduct == null)
            {
                return this.BadRequest("Invalid product.");
            }
            if (!Enum.TryParse(model.Type, out ProductType type))
            {
                return this.BadRequest("Invalid type.");
            }
            currentProduct.Description = model.Description;
            currentProduct.Name = model.Name;
            currentProduct.Price = model.Price;
            currentProduct.Type = type;

            this.dbService.Db().SaveChanges();

            return this.Redirect("/Products/Details?id=" + model.Id);
        }

        [Authorize("Admin")]
        public IActionResult Delete(string id)
        {
            var viewModel = new ProductBindingModel()
            {
                Id = id,
            };
            return this.View(viewModel);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult Delete(ProductBindingModel model)
        {
            var product = this.dbService.Db().Products.FirstOrDefault(p => p.Id == model.Id);

            if (product == null)
            {
                return BadRequest("Product does not exist.");
            }

            if (product.IsDeleted)
            {
                return BadRequest("Product already deleted");
            }

            product.IsDeleted = true;

            this.dbService.Db().SaveChanges();

            return this.Redirect("/");
        }

        [Authorize("Admin")]
        public IActionResult Order(string id)
        {
            var product = this.dbService.Db().Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if (product == null)
            {
                return BadRequest("Invalid product.");
            }

            // ReSharper disable once PossibleNullReferenceException
            var userId = this.dbService.Db().Users.FirstOrDefault(u => u.UserName == this.User.Identity.Name).Id;
            var order = new Order()
            {
                OrderedOn = DateTime.UtcNow,
                ProductId = product.Id,
                UserId = userId,
            };

            this.dbService.Db().Orders.Add(order);
            this.dbService.Db().SaveChanges();
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var product = this.dbService.Db().Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return BadRequest("Invalid product");
            }

            var viewModel = new ProductBindingModel()
            {
                Price = product.Price,
                Description = product.Description,
                Name = product.Name,
                Type = product.Type.ToString(),
                Id = product.Id
            };
            return this.View(viewModel);
        }

    }
}