using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<CityInfo> Cities { get; set; }

        public MyDbContext(DbContextOptions options)
        :base(options)
        {
            
        }

        public static void SeedData(MyDbContext context)
        {
            context.Cities.Add(new CityInfo
            {
                Name = "Sofia",
                Population = 2000000,
                Temperature = -2.5M,
            });

            context.Cities.Add(new CityInfo
            {
                Name = "Kyustendil",
                Population = 40000,
                Temperature = -10M,
            });

            context.Cities.Add(new CityInfo
            {
                Name = "Plovdiv",
                Population = 400000,
                Temperature = 4M,
            });

            context.SaveChanges();
        }
    }
}
