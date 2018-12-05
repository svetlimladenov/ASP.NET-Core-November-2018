using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels.Administration
{
    public class PromoteDemoteViewModel
    {
        public ICollection<UserViewModel> NonAdminUsers { get; set; }

        public ICollection<UserViewModel> AdminUsers { get; set; }
    }
}
