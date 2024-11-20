using Entities.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitController : ControllerBase
    {
        private readonly ILogger<MeasurementUnitController> _logger;
        private readonly IServiceMeasurementUnit _serviceMeasurementUnit;
        public MeasurementUnitController(ILogger<MeasurementUnitController> logger, IServiceMeasurementUnit serviceMeasurementUnit)
        {

            _logger = logger;
            _serviceMeasurementUnit = serviceMeasurementUnit;
        }
        [HttpGet("get-measurement-units")]
        public async Task<IActionResult> GetMeasurementUnits()
        {
            var measurementUnits = await _serviceMeasurementUnit.GetMeasurementUnitList();
            return Ok(measurementUnits);
        }
        [HttpGet("get-measurement-unit-by-id")]
        public async Task<IActionResult> GetMeasurementUnitById(int id)
        {
            var measurementUnit = await _serviceMeasurementUnit.GetMeasurementUnitById(id);
            return Ok(measurementUnit);
        }
        [HttpDelete("delete-measurement-unit/{id}")]
        public async Task<IActionResult> DeleteMeasurementUnit(int id)
        {
            await _serviceMeasurementUnit.DeleteMeasurementUnit(id);
            return Ok();
        }
        [HttpPost("create-measurement-unit")]
        public async Task<IActionResult> PostMeasurementUnit(MeasurementUnitDto measurementUnitDto)
        {
            await _serviceMeasurementUnit.CreateMeasurementUnit(measurementUnitDto);
            return Ok();
        }
        [HttpPost("update-measurement-unit")]
        public async Task<IActionResult> PutMeasurementUnit(MeasurementUnitDto measurementUnitDto)
        {
            var measurementUnit = _serviceMeasurementUnit.GetMeasurementUnitById(measurementUnitDto.MeasurementUnitId);
            if (measurementUnit is not null)
            {
                await _serviceMeasurementUnit.UpdateMeasurementUnit(measurementUnitDto);
                return Ok();
            }
            return BadRequest();
        }
    }
}
