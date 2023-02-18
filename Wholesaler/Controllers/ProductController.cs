using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.DataTransferObject;
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
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            if (!await _productService.CheckProductExists(id))
            {
                return NotFound($"Product with id {id} does not exist");
            }

            return Ok(await _productService.GetSingleProduct(id));
        }

        [HttpPost] 
        public async Task<IActionResult> AddProduct(CreateProductDto product)
        {
            await _productService.AddProduct(product);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product request)
        {
            if (!await _productService.CheckProductExists(id))
            {
                return NotFound($"Product with id {id} does not exist");
            }

            await _productService.UpdateProduct(id, request);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            if (!await _productService.CheckProductExists(id))
            {
                return NotFound($"Product with id {id} does not exist");
            }

            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
