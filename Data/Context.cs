using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Meal> Meal { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<Owner> Owner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Dish>().ToTable("Dish");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Owner>().ToTable("Owner");
        }

        
    }
}