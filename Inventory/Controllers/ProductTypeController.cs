using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ILogger<ProductTypeController> _logger;
        private readonly IServiceProductType _serviceProductType;
        public ProductTypeController(ILogger<ProductTypeController> logger, IServiceProductType serviceProductType)
        {

            _logger = logger;
            _serviceProductType = serviceProductType;
        }
        [HttpGet("get-product-types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var productTypes = await _serviceProductType.GetProductTypeList();
            return Ok(productTypes);
        }
        [HttpGet("get-product-type-by-id")]
        public async Task<IActionResult> GetProductTypeById(int id)
        {
            var productType = await _serviceProductType.GetProductTypeById(id);
            return Ok(productType);
        }
        [HttpDelete("delete-product-type/{id}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            await _serviceProductType.DeleteProductType(id);
            return Ok();
        }
        [HttpPost("create-product-type")]
        public async Task<IActionResult> PostProductType(ProductTypeDto productTypeDto)
        {
            await _serviceProductType.CreateProductType(productTypeDto);
            return Ok();
        }
        [HttpPost("update-product-type")]
        public async Task<IActionResult> PutProductType(ProductTypeDto productTypeDto)
        {
            var productType = _serviceProductType.GetProductTypeById(productTypeDto.ProductTypeId);
            if (productType is not null)
            {
                await _serviceProductType.UpdateProductType(productTypeDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
