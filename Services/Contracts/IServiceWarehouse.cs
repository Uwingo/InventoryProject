using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceWarehouse
    {
        Task<List<WarehouseDto>> GetWarehouseList();
        Task<WarehouseDto> GetWarehouseById(int id);
        Task CreateWarehouse(WarehouseDto warehouseDto);
        Task UpdateWarehouse(WarehouseDto warehouseDto);
        Task DeleteWarehouse(int id);
    }
}
