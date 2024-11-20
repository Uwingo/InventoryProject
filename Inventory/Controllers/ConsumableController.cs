using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumableController : ControllerBase
    {
        private readonly ILogger<ConsumableController> _logger;
        private readonly IServiceConsumable _serviceConsumable;
        public ConsumableController(ILogger<ConsumableController> logger, IServiceConsumable serviceConsumable)
        {

            _logger = logger;
            _serviceConsumable = serviceConsumable;
        }
        [HttpGet("get-consumables")]
        public async Task<IActionResult> GetConsumables()
        {
            var consumables = await _serviceConsumable.GetConsumableList();
            return Ok(consumables);
        }
        [HttpGet("get-consumable-by-id")]
        public async Task<IActionResult> GetConsumableById(int id)
        {
            var consumable = await _serviceConsumable.GetConsumableById(id);
            return Ok(consumable);
        }
        [HttpDelete("delete-consumable/{id}")]
        public async Task<IActionResult> DeleteConsumable(int id)
        {
            var consumable = _serviceConsumable.GetConsumableById(id);
            if (consumable is not null)
            {
                await _serviceConsumable.DeleteConsumable(id);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("create-consumable")]
        public async Task<IActionResult> PostConsumable(ConsumableDto consumableDto)
        {
           
            await _serviceConsumable.CreateConsumable(consumableDto);
            return Ok();
        }
        [HttpPost("update-consumable")]
        public async Task<IActionResult> PutConsumable(ConsumableDto consumableDto)
        {
            var consumable = _serviceConsumable.GetConsumableById(consumableDto.ConsumableId);
            if (consumable is not null)
            {
                await _serviceConsumable.UpdateConsumable(consumableDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
