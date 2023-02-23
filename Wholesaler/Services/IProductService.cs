using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public interface IProductService
    {
        Task<PagedResult<Product>> GetAllProducts(ProductQuery? searchPhrase);
        Task<Product?> GetSingleProduct(int id);
        Task AddProduct(CreateProductDto product);
        Task UpdateProduct(int id, CreateProductDto product);
        Task DeleteProduct(int id);
        Task<bool> CheckProductExists(int id);
        Task<bool> CheckStorageExist(int id);
    }
}
