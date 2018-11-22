using System;
using System.Linq;
using Eventures.Data;
using Eventures.ViewModels.Orders;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Services.OrdersServices
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext db;

        public OrdersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public SingleOrderViewModel[] GetAllOrders()
        {
            return this.db.Orders.Select(x => new SingleOrderViewModel()
            {
                Event = x.Event.Name,
                Customer = x.Customer.UserName,
                OrderedOn = x.OrderedOn.ToString("d"),
            }).OrderByDescending(x => x.OrderedOn).ToArray();
        }
    }
}