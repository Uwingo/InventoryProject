using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceBrand
    {
        Task<List<BrandDto>> GetBrandList();
        Task<BrandDto> GetBrandById(int id);
        Task CreateBrand(BrandDto brandDto);
        Task UpdateBrand(BrandDto brandDto);
        Task DeleteBrand(int id);
    }
}
