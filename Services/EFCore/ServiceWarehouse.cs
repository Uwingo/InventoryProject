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
    public class ServiceWarehouse : IServiceWarehouse
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceWarehouse(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateWarehouse(WarehouseDto warehouseDto)
        {
            var ware = _mapper.Map<Warehouse>(warehouseDto);
            await _repository.Warehouse.Create(ware);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteWarehouse(int id)
        {
            var ware = _repository.Warehouse.GetById(id).Result;
            ware.IsDeleted = true;
            if (ware != null)
            {
                await _repository.Warehouse.Delete(ware);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<WarehouseDto> GetWarehouseById(int id)
        {
            var ware = await _repository.Warehouse.GetById(id);
            return _mapper.Map<WarehouseDto>(ware);
        }

        public async Task<List<WarehouseDto>> GetWarehouseList()
        {
            var wares = _repository.Warehouse.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<WarehouseDto>>(wares);
        }

        public async Task UpdateWarehouse(WarehouseDto warehouseDto)
        {
            var ware = await _repository.Warehouse.GetById(warehouseDto.WarehouseId);
           if (ware != null)
            {
                ware.WarehouseName = warehouseDto.WarehouseName;
                ware.IsMainWarehouse = warehouseDto.IsMainWarehouse;
                ware.MainWarehouse = warehouseDto.MainWarehouse;
                ware.UnitName = warehouseDto.UnitName;
                await _repository.Warehouse.Update(ware);
                await _repository.SaveChangesAsync();
            }
           
        }
    }
}
