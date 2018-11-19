using System.Linq;
using ChushkaToAsp.Services;
using ChushkaWebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChushkaToAsp.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IDbService dbService;

        public OrdersController(IDbService db)
        {
            this.dbService = db;
        }
        [Authorize("Admin")]
        public IActionResult Index()
        {
            var viewModel = new AllOrdersViewModel();

            var allOrders = this.dbService.Db().Orders.Select(o => new BaseOrderViewModel()
            {
                Customer = o.User.UserName,
                Product = o.Product.Name,
                Id = o.Id,
                OrderedOn = o.OrderedOn.ToString("d"),
            }).ToArray();

            viewModel.Orders = allOrders;

            return View(viewModel);
        }
    }
}
