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
            modelBuilder.Entity<Restaurant>().Property(r => r.Name).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<Address>().Property(a => a.City).IsRequired();
        }

    }
}
