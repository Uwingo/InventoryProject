using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceInventory
    {
        Task<List<InventoryDto>> GetInventoryList();
        Task<InventoryDto> GetInventoryById(int id);
        Task<int> CreateInventory(InventoryDto inventoryDto);
        Task UpdateInventory(InventoryDto inventoryDto);
        Task DeleteInventory(int id);
        Task<List<InventoryDto>> GetInventoriesByWarehouseId(int warehouseId);
    }
}
