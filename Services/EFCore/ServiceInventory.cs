using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class ServiceInventory:IServiceInventory
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceInventory(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateInventory(InventoryDto inventoryDto)
        {
            var inventory = _mapper.Map<InventoryStock>(inventoryDto);
            await _repository.Inventory.Create(inventory);
            await _repository.SaveChangesAsync();
            return inventory.InventoryStockId;
        }

        public async Task DeleteInventory(int id)
        {
            var inventory = _repository.Inventory.GetById(id).Result;
            inventory.IsDeleted = true;
            if (inventory != null)
            {
                await _repository.Inventory.Delete(inventory);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<InventoryDto> GetInventoryById(int id)
        {
            var inventory = await _repository.Inventory.GetById(id);
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<List<InventoryDto>> GetInventoryList()
        {
            var inventories = _repository.Inventory.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<InventoryDto>>(inventories);
        }

        public async Task UpdateInventory(InventoryDto inventoryDto)
        {
            var inventory = _mapper.Map<InventoryStock>(inventoryDto);
            await _repository.Inventory.Update(inventory);
            await _repository.SaveChangesAsync();
        }
        public async Task<List<InventoryDto>> GetInventoriesByWarehouseId(int warehouseId)
        {
            var inventories = await _repository.Inventory.Read(false);
            var filteredInventories = inventories.Where(i => i.WarehouseId_FK == warehouseId).ToList();
            return _mapper.Map<List<InventoryDto>>(filteredInventories);
        }

    }
}
