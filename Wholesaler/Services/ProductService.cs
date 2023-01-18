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

        public async Task<List<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
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

        public async Task<List<Product>?> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>?> UpdateProduct(int id, Product product)
        {
            var updateProduct = await _context.Products.FindAsync(id);
            if (updateProduct is null)
                return null;

            updateProduct.Id = product.Id;
            updateProduct.Name = product.Name;
            updateProduct.Description = product.Description;
            updateProduct.Unit = product.Unit;

            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }
    }
}
