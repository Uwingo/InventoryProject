using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryStockController : ControllerBase
    {
        private readonly ILogger<InventoryStockController> _logger;
        private readonly IServiceInventory _serviceInventory;
        private readonly IServiceStockChange _serviceStockChange;
        public InventoryStockController(ILogger<InventoryStockController> logger, IServiceInventory serviceInventory, IServiceStockChange serviceStockChange)
        {
            _serviceStockChange = serviceStockChange;
            _logger = logger;
           _serviceInventory = serviceInventory;
        }
        [HttpGet("get-inventories")]
        public async Task<IActionResult> GetInventories()
        {
            var inventories = await _serviceInventory.GetInventoryList();
            return Ok(inventories);
        }
        [HttpGet("get-inventory-by-id")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var inventory = await _serviceInventory.GetInventoryById(id);
            return Ok(inventory);
        }
        [HttpDelete("delete-inventory/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            await _serviceInventory.DeleteInventory(id);
            return Ok();
        }
        [HttpPost("create-inventory")]
        public async Task<IActionResult> PostInventory(InventoryDto inventoryDto)
        {
            var data = await _serviceInventory.CreateInventory(inventoryDto);
            StockChangeDto stockChange = new StockChangeDto();
            stockChange.InventoryId_FK = data;
            stockChange.WarehouseId_FK = inventoryDto.WarehouseId_FK;
            stockChange.Count = inventoryDto.Stock;
            stockChange.IsTransfer = false;
            await _serviceStockChange.CreateStockChange(stockChange);
            return Ok();
        }
        [HttpPut("update-inventory")]
        public async Task<IActionResult> PutInventory(InventoryDto inventoryDto)
        {
            var inventory = await _serviceInventory.GetInventoryById(inventoryDto.InventoryStockId);
            //depodan aktarılan için
            StockChangeDto stockChangeDto = new StockChangeDto { Count=inventoryDto.Stock ,InventoryId_FK=inventory.InventoryStockId,WarehouseId_FK=inventory.WarehouseId_FK,IsTransfer=false};   
            //yeni depoya giriş için
            StockChangeDto stockChange = new StockChangeDto();
            stockChange.InventoryId_FK = inventory.InventoryStockId;
            stockChange.WarehouseId_FK = inventoryDto.WarehouseId_FK;
            stockChange.Count = inventoryDto.Stock;
            stockChange.IsTransfer = true;
            await _serviceStockChange.CreateStockChange(stockChange);
            await _serviceStockChange.CreateStockChange(stockChangeDto);
            if (inventory is not null)
            {
                if (inventory.Stock > inventoryDto.Stock) {
                    inventory.Stock = inventory.Stock- inventoryDto.Stock;
                    await _serviceInventory.UpdateInventory(inventory);
                    inventoryDto.InventoryStockId = 0;
                    await _serviceInventory.CreateInventory(inventoryDto);
                    return Ok();
                } else {
                    await _serviceInventory.UpdateInventory(inventoryDto);
                    return Ok();
                }
               
            }
            return BadRequest();
        }
        [HttpGet("get-inventories-by-warehouse-id")]
        public async Task<IActionResult> GetInventoriesByWarehouseId(int warehouseId)
        {
            var inventories = await _serviceInventory.GetInventoriesByWarehouseId(warehouseId);
            return Ok(inventories);
        }


    }
}
