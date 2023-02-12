using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class StorageService : IStorageService
    {
        private readonly DataContext _context;

        public StorageService(DataContext context)
        {
            _context = context;
        }

        public async Task AddStorage(Storage storage)
        {
            _context.Storages.Add(storage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStorage(int id)
        {
            var storage = _context.Storages.First(st => st.Id == id);
            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Storage>> GetAllStorages()
        {
            return await _context.Storages.ToListAsync();
        }

        public async Task<Storage> GetSingleStorage(int id)
        {
            return await _context.Storages.FirstAsync(st => st.Id == id); 
        }

        public async Task UpdateStorage(int id, Storage storage)
        {
            var updateStorage = _context.Storages.First(st => st.Id == id);

            updateStorage.Id = storage.Id;
            updateStorage.Name = storage.Name;
            updateStorage.Address = storage.Address;
            updateStorage.City = storage.City;
            updateStorage.Type = storage.Type;
            updateStorage.Products = storage.Products;

            _context.Storages.Update(updateStorage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckStorageExist(int id)
        {
            var check = _context.Storages.Where(st => st.Id == id);
            return await check.AnyAsync();
        }
    }
}
