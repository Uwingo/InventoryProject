using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IServiceProduct _serviceProduct;
        public ProductController(ILogger<ProductController> logger, IServiceProduct serviceProduct)
        {

            _logger = logger;
            _serviceProduct = serviceProduct;
        }
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _serviceProduct.GetProductList();
            return Ok(products);
        }
        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceProduct.GetProductById(id);
            return Ok(product);
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _serviceProduct.GetProductById(id);
            if (product is not null)
            {
                await _serviceProduct.DeleteProduct(id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPost("create-product")]
        public async Task<IActionResult> PostProduct(ProductDto productDto)
        {
            await _serviceProduct.CreateProduct(productDto);
            return Ok();
        }
        [HttpPost("update-product")]
        public async Task<IActionResult> PutProduct(ProductDto productDto)
        {
            var product = _serviceProduct.GetProductById(productDto.ProductId);
            if (product is not null)
            {
                await _serviceProduct.UpdateProduct(productDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
