using System.Linq;
using MvcIntro.Data;

namespace MvcIntro.Services
{
    public interface IUsersService
    {
        int Count();
    }

    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int Count()
        {
            return this.db.Users.Count();
        }
    }
}
