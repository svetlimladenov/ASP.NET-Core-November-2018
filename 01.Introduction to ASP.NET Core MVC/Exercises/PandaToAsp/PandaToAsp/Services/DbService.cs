using Panda.Data;

namespace PandaToAsp.Services
{
    public class DbService : IDbService
    {
        private readonly ApplicationDbContext _db;

        public DbService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationDbContext Db()
        {
            return _db;
        }
    }
}
