using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels.Administration;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Services.AdministrationsServices
{
    public class AdministrationsService : IAdministrationsService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<EventureUser> userManager;

        public AdministrationsService(
            ApplicationDbContext db,
            UserManager<EventureUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }


        public ICollection<UserViewModel> GetAdminUsers(ClaimsPrincipal user)
        {
            var adminRoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin")?.Id;
            var adminRoles = this.db.UserRoles.Where(x => x.RoleId == adminRoleId).Select(x => new UserViewModel
            {
                Id = x.UserId,
                Username = this.db.Users.FirstOrDefault(u => u.Id == x.UserId).UserName,
            }).ToList();

            var currentUser = this.db.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Username = x.UserName
            }).FirstOrDefault(x => x.Username == user.Identity.Name);

            adminRoles.RemoveAll(x => x.Id == currentUser?.Id);
            return adminRoles;
        }

        public ICollection<UserViewModel> GetNonAdminUsers(ClaimsPrincipal user)
        {
            var nonAdminRoleId = db.Roles.FirstOrDefault(x => x.Name != "Admin")?.Id;
            var nonAdminRoles = this.db.UserRoles.Where(x => x.RoleId == nonAdminRoleId).Select(x => new UserViewModel
            {
                Id = x.UserId,
                Username = this.db.Users.FirstOrDefault(u => u.Id == x.UserId).UserName,
            }).ToList();

            var currentUser = this.db.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                Username = x.UserName
            }).FirstOrDefault(x => x.Username == user.Identity.Name);

            nonAdminRoles.RemoveAll(x => x.Id == currentUser?.Id);

            return nonAdminRoles;

        }

        public void PromoteUser(string id)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == id) as EventureUser;
            userManager.RemoveFromRoleAsync(user, "User").GetAwaiter().GetResult();
            this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

        }

        public void DemoteUser(string id)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == id) as EventureUser;
            userManager.RemoveFromRoleAsync(user, "Admin").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();

        }
    }
}
