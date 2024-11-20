using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        IServiceBrand serviceBrand { get; }
        IServiceConsumable serviceConsumable { get; }
        IServiceInventory serviceInventory { get; }
        IServiceMeasurementUnit serviceMeasurementUnit { get; }
        IServiceModel serviceModel { get; }
        IServiceProduct serviceProduct { get; }
        IServiceProductType serviceProductType { get; }
        IServiceStockChange serviceStockChange { get; }
        IServiceSupplier serviceSupplier { get; }
        IServiceWarehouse serviceWarehouse { get; }
    }
}
