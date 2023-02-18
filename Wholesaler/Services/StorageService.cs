using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class StorageService : IStorageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StorageService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddStorage(CreateStorageDto storage)
        {
            var storageDto = _mapper.Map<Storage>(storage);
            _context.Storages.Add(storageDto);
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

        public async Task UpdateStorage(int id, CreateStorageDto storage)
        {
            var updateStorage = _context.Storages.First(st => st.Id == id);

            updateStorage.Name = storage.Name;
            updateStorage.Address = storage.Address;
            updateStorage.City = storage.City;
            updateStorage.Type = storage.Type;

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
