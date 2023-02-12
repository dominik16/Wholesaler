using Wholesaler.Models;

namespace Wholesaler.Services
{
    public interface IStorageService
    {
        Task<List<Storage>> GetAllStorages();
        Task<Storage> GetSingleStorage(int id);
        Task AddStorage(Storage storage);
        Task UpdateStorage(int id, Storage storage);
        Task DeleteStorage(int id);
        Task<bool> CheckStorageExist(int id);
    }
}
