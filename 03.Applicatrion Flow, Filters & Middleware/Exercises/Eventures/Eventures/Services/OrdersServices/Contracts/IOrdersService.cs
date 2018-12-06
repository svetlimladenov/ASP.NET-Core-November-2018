using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.ViewModels.Orders;

namespace Eventures.Services.OrdersServices
{
    public interface IOrdersService
    {
        SingleOrderViewModel[] GetAllOrders();
    }
}
