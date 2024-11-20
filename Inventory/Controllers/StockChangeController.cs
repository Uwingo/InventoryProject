using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockChangeController : ControllerBase
    {
        private readonly ILogger<StockChangeController> _logger;
        private readonly IServiceStockChange _serviceStockChange;
        public StockChangeController(ILogger<StockChangeController> logger, IServiceStockChange serviceStockChange)
        {

            _logger = logger;
            _serviceStockChange = serviceStockChange;
        }
        [HttpGet("get-stock-changes")]
        public async Task<IActionResult> GetStockChanges()
        {
            var stockChanges = await _serviceStockChange.GetStockChangeList();
            return Ok(stockChanges);
        }
        [HttpGet("get-stok-change-by-id")]
        public async Task<IActionResult> GetStockChangeById(int id)
        {
            var stockChange = await _serviceStockChange.GetStockChangeById(id);
            return Ok(stockChange);
        }
        [HttpDelete("delete-stock-change/{id}")]
        public async Task<IActionResult> DeleteStockChange(int id)
        {
            await _serviceStockChange.DeleteStockChange(id);
            return Ok();
        }
        [HttpPost("create-stock-change")]
        public async Task<IActionResult> PostStockChange(StockChangeDto stockChangeDto)
        {
            await _serviceStockChange.CreateStockChange(stockChangeDto);
            return Ok();
        }
        [HttpPut("update-stock-change")]
        public async Task<IActionResult> PutStockChange(StockChangeDto stockChangeDto)
        {
            var stockChange = _serviceStockChange.GetStockChangeById(stockChangeDto.StockChangeId);
            if (stockChange is not null)
            {
                await _serviceStockChange.UpdateStockChange(stockChangeDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
