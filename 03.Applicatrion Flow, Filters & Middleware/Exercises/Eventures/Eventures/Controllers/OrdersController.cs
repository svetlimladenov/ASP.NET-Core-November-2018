using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Services.OrdersServices;
using Eventures.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IActionResult All()
        {
            var viewModel = new AllOrdersViewModel();
            viewModel.Orders = ordersService.GetAllOrders();
            return View(viewModel);
        }
    }
}