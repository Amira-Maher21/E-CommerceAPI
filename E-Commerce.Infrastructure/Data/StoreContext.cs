using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        // DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-2I0OH7I\\SQLEXPRESS;Initial Catalog= e-commerce.System; User Id=sa;Password=123;TrustServerCertificate=true")
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

    }
}
