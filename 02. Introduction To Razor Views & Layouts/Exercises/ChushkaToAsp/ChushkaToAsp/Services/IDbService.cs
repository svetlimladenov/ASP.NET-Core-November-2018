using Chushka.Data;

namespace ChushkaToAsp.Services
{
    public interface IDbService
    {
        ApplicationDbContext Db();
    }
}
