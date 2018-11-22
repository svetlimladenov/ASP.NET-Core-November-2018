using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels.Orders
{
    public class AllOrdersViewModel
    {
        public ICollection<SingleOrderViewModel> Orders { get; set; }
    }
}
