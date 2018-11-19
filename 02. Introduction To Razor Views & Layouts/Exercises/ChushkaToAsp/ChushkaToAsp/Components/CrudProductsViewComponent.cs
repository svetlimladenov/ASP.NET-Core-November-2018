using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChushkaToAsp.Services;
using ChushkaToAsp.ViewModels.Products;
using ChushkaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChushkaToAsp.Components
{
    [ViewComponent(Name = "CrudProducts")]
    public class CrudProductsViewComponent : ViewComponent
    {
        private readonly IDbService dbService;

        public CrudProductsViewComponent(IDbService dbService)
        {
            this.dbService = dbService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string actionName, string id)
        {
            var product = this.dbService.Db().Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return this.View(new ProductBindingModel() { Action = actionName });
            }

            var viewModel = new ProductBindingModel()
            {
                Action = actionName,
                Description = product.Description,
                Name = product.Name,
                Type = product.Type.ToString(),
                Price = product.Price,
                Id = product.Id
            };
            return View(viewModel);

        }

    }
}
