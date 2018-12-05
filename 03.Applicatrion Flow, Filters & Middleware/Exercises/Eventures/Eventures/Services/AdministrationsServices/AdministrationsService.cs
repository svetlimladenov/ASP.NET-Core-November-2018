using System.Collections.Generic;
using System.Linq;
using Eventures.Data;
using Eventures.ViewModels.Administration;

namespace Eventures.Services.AdministrationsServices
{
    public class AdministrationsService : IAdministrationsService
    {
        private readonly ApplicationDbContext db;

        public AdministrationsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public ICollection<UserViewModel> GetAdminUsers()
        {
            var adminRoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin").Id;
            return this.db.UserRoles.Where(x => x.RoleId == adminRoleId).Select(x => new UserViewModel
            {
                Id = x.UserId,
                Username = this.db.Users.FirstOrDefault(u => u.Id == x.UserId).UserName,
            }).ToArray();
        }

        public ICollection<UserViewModel> GetNonAdminUsers()
        {
            var nonAdminRoleId = db.Roles.FirstOrDefault(x => x.Name != "Admin").Id;
            return this.db.UserRoles.Where(x => x.RoleId == nonAdminRoleId).Select(x => new UserViewModel
            {
                Id = x.UserId,
                Username = this.db.Users.FirstOrDefault(u => u.Id == x.UserId).UserName,
            }).ToArray();
        }
    }
}
