using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product?> GetSingleProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return null;
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FirstAsync(prd => prd.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(int id, Product product)
        {
            var updateProduct = await _context.Products.FirstAsync(prd => prd.Id == id);

            updateProduct.Id = product.Id;
            updateProduct.Name = product.Name;
            updateProduct.Description = product.Description;
            updateProduct.Unit = product.Unit;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckProductExists(int id)
        {
            var check = _context.Products.Where(prd => prd.Id == id);
            return await check.AnyAsync();
        }
    }
}
