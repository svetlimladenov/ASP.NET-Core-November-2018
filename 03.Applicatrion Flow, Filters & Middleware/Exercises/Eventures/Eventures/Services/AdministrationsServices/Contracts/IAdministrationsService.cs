using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventures.ViewModels.Administration;

namespace Eventures.Services.AdministrationsServices
{
    public interface IAdministrationsService
    {
        ICollection<UserViewModel> GetAdminUsers(ClaimsPrincipal user);

        ICollection<UserViewModel> GetNonAdminUsers(ClaimsPrincipal user);

        void PromoteUser(string id);

        void DemoteUser(string id);
    }
}
