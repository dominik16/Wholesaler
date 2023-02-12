using Microsoft.EntityFrameworkCore;
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
             new Product { Id = 4, Name = "Kabel RC", Description = "Kabel wysokonapięciowy", Price = 800, StorageId = 2, Unit = "m/b" },
             new Product { Id = 5, Name = "Nóż do tapet", Description = "Narzędzie - nóż", Price = 1.99, StorageId = 1, Unit = "szt" }
            );

            modelBuilder.Entity<Storage>().HasData(
             new Storage { Id = 1, Name = "Magazyn Wewnętrzny", Address = "Graniczna 12", City = "Kraków", Type = "Detaliczny"},
             new Storage { Id = 2, Name = "Magazyn Zewnętrzny Zadaszony", Address = "Wesoła 46", City = "Rzeszów", Type = "Hurtowy"},
             new Storage { Id = 3, Name = "Magazyn Niezadaszony", Address = "Słoneczna 2", City = "Gdańsk", Type = "Hurtowy"}
            );
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }
    }
}