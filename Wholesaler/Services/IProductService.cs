using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetSingleProduct(int id);
        Task AddProduct(CreateProductDto product);
        Task UpdateProduct(int id, CreateProductDto product);
        Task DeleteProduct(int id);
        Task<bool> CheckProductExists(int id);
        Task<bool> CheckStorageExist(int id);
    }
}
