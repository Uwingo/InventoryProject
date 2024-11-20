using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IRepositoryBrand Brand { get; }
        IRepositoryConsumable Consumable { get; }
        IRepositoryInventory Inventory { get; }
        IRepositoryMeasurementUnit MeasurementUnit { get; }
        IRepositoryModel Model { get; }
        IRepositoryProduct Product { get; }
        IRepositoryProductType ProductType { get; }
        IRepositoryStockChange StockChange { get; }
        IRepositorySupplier Supplier { get; }
        IRepositoryWarehouse Warehouse { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
