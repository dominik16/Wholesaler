using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public interface IStorageService
    {
        Task<List<Storage>> GetAllStorages();
        Task<Storage> GetSingleStorage(int id);
        Task AddStorage(CreateStorageDto storage);
        Task UpdateStorage(int id, CreateStorageDto storage);
        Task DeleteStorage(int id);
        Task<bool> CheckStorageExist(int id);
    }
}
