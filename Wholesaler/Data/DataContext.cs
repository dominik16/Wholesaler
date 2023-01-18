using Microsoft.EntityFrameworkCore;
using Wholesaler.Models;

namespace Wholesaler.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseNpgsql("Server=.\\SQLExpress;Database=WholesalerDb; trusted_connection=true;");
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;");
        }

        public DbSet<Product> Products { get; set; }
    }
}

