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
            if(!await _productService.CheckStorageExist(product.StorageId))
            {
                return BadRequest($"Erro while adding product, storage with id: {product.StorageId} doesn't exist");
            }

            await _productService.AddProduct(product);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, CreateProductDto request)
        {
            if (!await _productService.CheckProductExists(id))
            {
                return NotFound($"Product with id {id} does not exist");
            }

            if (!await _productService.CheckStorageExist(request.StorageId))
            {
                return BadRequest($"Erro while adding product, storage with id: {request.StorageId} doesn't exist");
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
