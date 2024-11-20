using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ILogger<ModelController> _logger;
        private readonly IServiceModel _serviceModel;
        public ModelController(ILogger<ModelController> logger, IServiceModel serviceModel)
        {

            _logger = logger;
            _serviceModel = serviceModel;
        }
        [HttpGet("get-models")]
        public async Task<IActionResult> GetModels()
        {
            var models = await _serviceModel.GetModelList();
            return Ok(models);
        }
        [HttpGet("get-model-by-id")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var model = await _serviceModel.GetModelById(id);
            return Ok(model);
        }
        [HttpDelete("delete-model/{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = _serviceModel.GetModelById(id);
            if (model is not null)
            {
                await _serviceModel.DeleteModel(id);
                return Ok();
            }
            return BadRequest();

        }
        [HttpPost("create-model")]
        public async Task<IActionResult> PostModel(ModelDto modelDto)
        {
            await _serviceModel.CreateModel(modelDto);
            return Ok();
        }
        [HttpPost("update-model")]
        public async Task<IActionResult> PutModel(ModelDto modelDto)
        {
            var model = _serviceModel.GetModelById(modelDto.ModelId).Result;
            if (model is not null)
            {                
                await _serviceModel.UpdateModel(modelDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
