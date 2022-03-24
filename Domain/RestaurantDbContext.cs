using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().Property(r => r.Name).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Restaurant>().Property(r => r.HasDelivery).IsRequired();

            modelBuilder.Entity<Address>().Property(a => a.PostalCode).HasMaxLength(6).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.City).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Street).HasMaxLength(35).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.EstateNumber).HasMaxLength(6).IsRequired();

            modelBuilder.Entity<Dish>().Property(d => d.Name).HasMaxLength(25).IsRequired();
            modelBuilder.Entity<Dish>().Property(d => d.Price).IsRequired();
        }

    }
}
