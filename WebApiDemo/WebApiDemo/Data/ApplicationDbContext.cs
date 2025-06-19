using Microsoft.EntityFrameworkCore;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Shirt>()
                .HasData(
                    new Shirt
                    {
                        Id = 1,
                        Color = "Red",
                        Size = 10,
                        Brand = "Nike",
                        Gender = "Male",
                        Price = 100,
                    },
                    new Shirt
                    {
                        Id = 2,
                        Color = "Blue",
                        Size = 12,
                        Brand = "Adidas",
                        Gender = "Female",
                        Price = 200,
                    },
                    new Shirt
                    {
                        Id = 3,
                        Color = "Green",
                        Size = 14,
                        Brand = "Puma",
                        Gender = "Male",
                        Price = 300,
                    }
                );
        }
    }
}
