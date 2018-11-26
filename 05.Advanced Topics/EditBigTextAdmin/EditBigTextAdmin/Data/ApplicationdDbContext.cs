using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EditBigTextAdmin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EditBigTextAdmin.Data
{
    public class ApplicationdDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationdDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Text> Texts { get; set; }
    }
}
