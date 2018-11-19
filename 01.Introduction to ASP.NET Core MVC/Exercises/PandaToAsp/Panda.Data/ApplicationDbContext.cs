using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Panda.Models;

namespace Panda.Data
{
    public class ApplicationDbContext : IdentityDbContext<PandaUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Receipt>(r => r.HasKey(x => x.Id));

            builder.Entity<Receipt>()
                .HasOne(r => r.Recipient)
                .WithMany(u => u.Receipts)
                .HasForeignKey(r => r.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
