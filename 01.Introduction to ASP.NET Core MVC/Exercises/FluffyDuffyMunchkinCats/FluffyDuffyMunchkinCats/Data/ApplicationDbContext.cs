using FluffyDuffyMunchkinCats.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FluffyDuffyMunchkinCats.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
    }
}
