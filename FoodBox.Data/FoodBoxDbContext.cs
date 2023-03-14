using FoodBox.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Data
{
    public class FoodBoxDbContext: IdentityDbContext <Employee>
    {
        public FoodBoxDbContext()
        {

        }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Product>Products { get; set;}  
        public DbSet<Store> Stores { get; set;}
        public DbSet<StoreProduct> StoreProducts { get; set;}
        public DbSet<StoreUser> StoreUsers { get; set;}

        public FoodBoxDbContext(DbContextOptions<FoodBoxDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
