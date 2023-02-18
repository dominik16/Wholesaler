using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wholesaler.Data;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;

namespace Wholesaler.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProduct(CreateProductDto product)
        {
            var productDto = _mapper.Map<Product>(product);
            _context.Products.Add(productDto);
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

        public async Task UpdateProduct(int id, CreateProductDto product)
        {
            var updateProduct = await _context.Products.FirstAsync(prd => prd.Id == id);

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
