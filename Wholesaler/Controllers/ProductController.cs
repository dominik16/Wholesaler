using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.Models;
using Wholesaler.Services;

namespace Wholesaler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            var result = await _productService.GetSingleProduct(id);
            if (result is null)
                return NotFound("Sorry, but this product doesn't exist");
            return Ok(result);
        }

        [HttpPost] 
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            var result = await _productService.AddProduct(product);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product request)
        {
            var result = await _productService.UpdateProduct(id, request);
            if (result is null)
                return NotFound("Sorry, but this product doesn't exist");

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result is null)
                return NotFound("Sorry, but this product doesn't exist");
            
            return Ok(result);
        }
    }
}
