using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.ViewModels.Administration;

namespace Eventures.Services.AdministrationsServices
{
    public interface IAdministrationsService
    {
        ICollection<UserViewModel> GetAdminUsers();

        ICollection<UserViewModel> GetNonAdminUsers();

    }
}
