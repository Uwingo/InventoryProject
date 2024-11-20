using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Services.Contracts;
using Services.EFCore;

namespace Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly ILogger<BrandController> _logger;
        private readonly IServiceBrand _serviceBrand;
        public BrandController(ILogger<BrandController> logger, IServiceBrand serviceBrand)
        {

            _logger = logger;
            _serviceBrand = serviceBrand;
        }
        [HttpGet("get-brands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _serviceBrand.GetBrandList();
            return Ok(brands);
        }
        [HttpGet("get-brand-by-id")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _serviceBrand.GetBrandById(id);
            return Ok(brand);
        }
        [HttpDelete("delete-brand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _serviceBrand.DeleteBrand(id);
            return Ok();
        }
        [HttpPost("create-brand")]
        public async Task<IActionResult> PostBrand(BrandDto brandDto)
        {
            await _serviceBrand.CreateBrand(brandDto);
            return Ok();
        }
        [HttpPost("update-brand")]
        public async Task<IActionResult> PutBrand(BrandDto brandDto)
        {
            var brand = _serviceBrand.GetBrandById(brandDto.BrandId);
            if (brand is not null)
            {
                await _serviceBrand.UpdateBrand(brandDto);
                return Ok();
            }
            return BadRequest();
        }


    }
}
