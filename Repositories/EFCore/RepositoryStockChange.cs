using Entities.Model;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public  class RepositoryStockChange:RepositoryBase<StockChange>,IRepositoryStockChange
    {
        private readonly RepositoryContext _context;
        public RepositoryStockChange(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<object> GetStockChange()
        {
            var result = from StockChanges in _context.StockChanges
                         join Warehouse in _context.Warehouses
                         on StockChanges.WarehouseId_FK equals Warehouse.WarehouseId
                         join Inventories in _context.Inventories
                         on StockChanges.InventoryId_FK equals Inventories.InventoryStockId
                         select new
                         {
                             StockChange = StockChanges,
                             warehouse = Warehouse.WarehouseName,
                             inventoriesDes = Inventories.InventoryDescription,
                             inventories = Inventories.InventoryStockId
                         };
            return result;
        }
    }
}
