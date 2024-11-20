using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.EFCore
{
    public class ServiceBrand : IServiceBrand
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceBrand(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateBrand(BrandDto brandDto)
        {
            var brand = _mapper.Map<Brand>(brandDto);
            await _repository.Brand.Create(brand);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteBrand(int id)
        {
            var brand = _repository.Brand.GetById(id).Result;
            brand.IsDeleted = true;
            if (brand != null)
            {
                await _repository.Brand.Delete(brand);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<BrandDto> GetBrandById(int id)
        {
            var brand = await _repository.Brand.GetById(id);

            return _mapper.Map<BrandDto>(brand);
        }

        public async Task<List<BrandDto>> GetBrandList()
        {
            var brands =  _repository.Brand.Read(false).Result.Where(w=>w.IsDeleted==false).ToList();          
            return _mapper.Map<List<BrandDto>>(brands);
        }

        public async Task UpdateBrand(BrandDto brandDto)
        {
            var brand = await _repository.Brand.GetById(brandDto.BrandId);
            if (!brand.IsDeleted)
            {
                brand.BrandName = brandDto.BrandName;
                await _repository.Brand.Update(brand);
                await _repository.SaveChangesAsync();
            }

        }
    }
}
