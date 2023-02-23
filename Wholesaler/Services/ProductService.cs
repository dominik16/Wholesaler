using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PagedResult<Product>> GetAllProducts(ProductQuery? query)
        {
            var baseQueryToList = await _context.Products.ToListAsync();

            var baseQuery = baseQueryToList.Where(pr => query.SearchPhrase == null || (pr.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                                || pr.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

            //if (string.IsNullOrEmpty(query.SortBy))
            //{
            //    var columnsSelectors = new Dictionary<string, Expression<Func<Product, object>>>()
            //    {
            //        { nameof(Product.Name), pr => pr.Name },
            //        { nameof(Product.Price), pr => pr.Price }
            //    };

            //    var selectedColumn = columnsSelectors[query.SortBy];

            //    baseQuery = query.SortDirection == SortDirection.ASC
            //        ? baseQuery.OrderBy(selectedColumn)
            //        : baseQuery.OrderByDescending(selectedColumn)
            //        .ToList();
            //}

            var products = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var result = new PagedResult<Product>(products, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
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

        public async Task<bool> CheckStorageExist(int id)
        {
            var check = _context.Storages.Where(st => st.Id == id);
            return await check.AnyAsync();
        }
    }
}
