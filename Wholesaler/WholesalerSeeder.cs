using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;

namespace Wholesaler
{
    public class WholesalerSeeder
    {
        private readonly DataContext _context;

        public WholesalerSeeder(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.IsRelational())
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }
            }  
        }
    }
}
