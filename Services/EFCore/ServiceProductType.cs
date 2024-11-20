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

namespace Services.EFCore
{
    public class ServiceProductType : IServiceProductType
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceProductType(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateProductType(ProductTypeDto productTypeDto)
        {
            var type = _mapper.Map<ProductType>(productTypeDto);
            await _repository.ProductType.Create(type);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteProductType(int id)
        {
            var type = _repository.ProductType.GetById(id).Result;
            type.IsDeleted = true;
            if (type != null)
            {
                await _repository.ProductType.Delete(type);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<ProductTypeDto> GetProductTypeById(int id)
        {
            var type = await _repository.ProductType.GetById(id);
            return _mapper.Map<ProductTypeDto>(type);
        }

        public async Task<List<ProductTypeDto>> GetProductTypeList()
        {
            var types = _repository.ProductType.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<ProductTypeDto>>(types);
        }

        public async Task UpdateProductType(ProductTypeDto productTypeDto)
        {
            var type = await _repository.ProductType.GetById(productTypeDto.ProductTypeId);
            if (type != null)
            {
                type.ProductTypeDescription = productTypeDto.ProductTypeDescription;
                type.ProductTypeName = productTypeDto.ProductTypeName;
                type.MeasurementUnitId_FK= productTypeDto.MeasurementUnitId_FK;
                await _repository.ProductType.Update(type);
                await _repository.SaveChangesAsync();
            }

        }
    }
}
