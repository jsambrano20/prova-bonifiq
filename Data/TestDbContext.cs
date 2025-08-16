using Bogus;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using System.Collections.Generic;

namespace ProvaPub.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(GetCustomerSeed());
            modelBuilder.Entity<Product>().HasData(GetProductSeed());

            modelBuilder.Entity<RandomNumber>()
                        .HasIndex(r => r.Number)
                        .IsUnique();
        }

        private Customer[] GetCustomerSeed()
        {
            var faker = new Faker();
            var result = new List<Customer>();

            for (int i = 1; i <= 20; i++)
            {
                result.Add(new Customer
                {
                    Id = i,
                    Name = faker.Person.FullName
                });
            }

            return result.ToArray();
        }

        private Product[] GetProductSeed()
        {
            var faker = new Faker();
            var result = new List<Product>();

            for (int i = 1; i <= 20; i++)
            {
                result.Add(new Product
                {
                    Id = i,
                    Name = faker.Commerce.ProductName()
                });
            }

            return result.ToArray();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RandomNumber> Numbers { get; set; }
    }
}
