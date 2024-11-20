using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly IServiceWarehouse _serviceWarehouse;
        public WarehouseController(ILogger<WarehouseController> logger, IServiceWarehouse serviceWarehouse)
        {

            _logger = logger;
            _serviceWarehouse = serviceWarehouse;
        }
        [HttpGet("get-warehouses")]
        public async Task<IActionResult> GetWarehouses()
        {
            var warehouses = await _serviceWarehouse.GetWarehouseList();
            return Ok(warehouses);
        }
        [HttpGet("get-warehouse-by-id")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _serviceWarehouse.GetWarehouseById(id);
            return Ok(warehouse);
        }
        [HttpDelete("delete-warehouse/{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            await _serviceWarehouse.DeleteWarehouse(id);
            return Ok();
        }
        [HttpPost("create-warehouse")]
        public async Task<IActionResult> PostWarehouse(WarehouseDto warehouseDto)
        {
            await _serviceWarehouse.CreateWarehouse(warehouseDto);
            return Ok();
        }
        [HttpPost("update-warehouse")]
        public async Task<IActionResult> PutWarehouse(WarehouseDto warehouseDto)
        {
            var warehouse = _serviceWarehouse.GetWarehouseById(warehouseDto.WarehouseId);
            if (warehouse is not null)
            {
                await _serviceWarehouse.UpdateWarehouse(warehouseDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
