using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Wholesaler.Models;

namespace Wholesaler.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
             new Product { Id = 1, Name = "Kabel YDY", Description = "Kabel prądowy", Price = 99.99, StorageId = 1, Unit = "m/b" },
             new Product { Id = 2, Name = "Żarówka 100W", Description = "Żarówka do świecenia", Price = 10.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 3, Name = "Śrubokręt", Description = "Śrubokręt krzyżakowy", Price = 5, StorageId = 2, Unit = "szt" },
             new Product { Id = 4, Name = "Kabel RC", Description = "Kabel wysokonapięciowy", Price = 800, StorageId = 3, Unit = "m/b" },
             new Product { Id = 5, Name = "Nóż do tapet", Description = "Narzędzie - nóż", Price = 1.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 6, Name = "Nożyce", Description = "Nożyce do drutu", Price = 99.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 7, Name = "Młotek duży", Description = "Młot duży", Price = 10.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 8, Name = "Śrubokręt płaski", Description = "Śrubokręt płaski krótki", Price = 5, StorageId = 2, Unit = "szt" },
             new Product { Id = 9, Name = "Kabel YDY 3x1.5", Description = "Kabel prądowy", Price = 800, StorageId = 2, Unit = "m/b" },
             new Product { Id = 10, Name = "Kabel YDY 2x1.5", Description = "Kabel prądowy", Price = 100.99, StorageId = 3, Unit = "m/bt" },
             new Product { Id = 11, Name = "Kabel YDY 4x1.5", Description = "Kabel prądowy", Price = 199.99, StorageId = 1, Unit = "m/b" },
             new Product { Id = 12, Name = "Kabel YDY 5x1.5", Description = "Kabel prądowy", Price = 299.99, StorageId = 1, Unit = "m/b" },
             new Product { Id = 13, Name = "Kabel YDY 2x2.5", Description = "Kabel prądowy", Price = 50.99, StorageId = 1, Unit = "m/b" },
             new Product { Id = 14, Name = "Kabel YDY 3x2.5", Description = "Śrubokręt krzyżakowy", Price = 105, StorageId = 2, Unit = "m/b" },
             new Product { Id = 15, Name = "Kabel YDY 4x2.5", Description = "Kabel wysokonapięciowy", Price = 150, StorageId = 2, Unit = "m/b" },
             new Product { Id = 16, Name = "Kabel YDY 5x2.5", Description = "Narzędzie - nóż", Price = 250, StorageId = 1, Unit = "m/b" },
             new Product { Id = 17, Name = "Złączka", Description = "Złączka do kabla", Price = 9.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 18, Name = "Złączka duża", Description = "Złączka - duża", Price = 20.99, StorageId = 1, Unit = "szt" },
             new Product { Id = 19, Name = "Żarówka LED 10 W", Description = "Żarówka LED", Price = 20, StorageId = 2, Unit = "szt" },
             new Product { Id = 20, Name = "Żarówka LED 5 W", Description = "Żarówka LED", Price = 10, StorageId = 2, Unit = "szt" }
            );

            modelBuilder.Entity<Storage>().HasData(
             new Storage { Id = 1, Name = "Magazyn Wewnętrzny", Address = "Graniczna 12", City = "Kraków", Type = "Detaliczny" },
             new Storage { Id = 2, Name = "Magazyn Zewnętrzny Zadaszony", Address = "Wesoła 46", City = "Rzeszów", Type = "Hurtowy" },
             new Storage { Id = 3, Name = "Magazyn Niezadaszony", Address = "Słoneczna 2", City = "Gdańsk", Type = "Hurtowy" }
            );

            modelBuilder.Entity<Role>().HasData(
             new Role { Id = 1, Name = "User" },
             new Role { Id = 2, Name = "Manager" },
             new Role { Id = 3, Name = "Admin" }
            );

            modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

            modelBuilder.Entity<Storage>()
            .HasKey(st => st.Id);

            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

            modelBuilder.Entity<Role>()
            .HasKey(r => r.Id);

            modelBuilder.Entity<User>(us =>
            {
                us.Property(u => u.Email).IsRequired();
                us.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Role>()
                .Property(r => r.Name).IsRequired();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    }
}