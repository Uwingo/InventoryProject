using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly IServiceSupplier _serviceSupplier;
        public SupplierController(ILogger<SupplierController> logger, IServiceSupplier serviceSupplier)
        {

            _logger = logger;
            _serviceSupplier = serviceSupplier;
        }
        [HttpGet("get-suppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _serviceSupplier.GetSupplierList();
            return Ok(suppliers);
        }
        [HttpGet("get-supplier-by-id")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var supplier = await _serviceSupplier.GetSupplierById(id);
            return Ok(supplier);
        }
        [HttpDelete("delete-supplier/{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = _serviceSupplier.GetSupplierById(id);
            if (supplier is not null)
            {
                await _serviceSupplier.DeleteSupplier(id);
                return Ok();
            }
            return BadRequest();
           
        }
        [HttpPost("create-supplier")]
        public async Task<IActionResult> PostSupplier(SupplierDto supplierDto)
        {
            await _serviceSupplier.CreateSupplier(supplierDto);
            return Ok();
        }
        [HttpPost("update-supplier")]
        public async Task<IActionResult> PutSupplier(SupplierDto supplierDto)
        {
            var supplier = _serviceSupplier.GetSupplierById(supplierDto.SupplierId);
            if (supplier is not null)
            {
                await _serviceSupplier.UpdateSupplier(supplierDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
