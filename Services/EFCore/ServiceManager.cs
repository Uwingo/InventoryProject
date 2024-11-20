using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class ServiceManager : IServiceManager
    {
        private readonly IConfiguration _configuration;
        private readonly Lazy<IServiceBrand> _serviceBrand;
        private readonly Lazy<IServiceConsumable> _serviceConsumable;
        private readonly Lazy<IServiceInventory> _serviceInventory;
        private readonly Lazy<IServiceMeasurementUnit> _serviceMeasurementUnit;
        private readonly Lazy<IServiceModel> _serviceModel;
        private readonly Lazy<IServiceProduct> _serviceProduct;
        private readonly Lazy<IServiceProductType> _serviceProductType;
        private readonly Lazy<IServiceStockChange> _serviceStockChange;
        private readonly Lazy<IServiceSupplier> _serviceSupplier;
        private readonly Lazy<IServiceWarehouse> _serviceWarehouse;

        public ServiceManager(IRepositoryManager repository, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceBrand = new Lazy<IServiceBrand>(() => new ServiceBrand(repository, mapper));
            _serviceConsumable = new Lazy<IServiceConsumable>(() => new ServiceConsumable(repository, mapper));
            _serviceInventory= new Lazy<IServiceInventory>(() => new ServiceInventory(repository, mapper));
            _serviceMeasurementUnit= new Lazy<IServiceMeasurementUnit>(() => new ServiceMeasurementUnit(repository, mapper));
            _serviceModel= new Lazy<IServiceModel>(()=>  new ServiceModel(repository, mapper));
            _serviceProduct= new Lazy<IServiceProduct>(() => new ServiceProduct(repository, mapper));
            _serviceProductType = new Lazy<IServiceProductType>(() => new ServiceProductType(repository, mapper));
            _serviceStockChange = new Lazy<IServiceStockChange>(() => new ServiceStockChange(repository, mapper));
            _serviceSupplier= new Lazy<IServiceSupplier>(() => new ServiceSupplier(repository, mapper));
            _serviceWarehouse= new Lazy<IServiceWarehouse>(() => new ServiceWarehouse(repository, mapper));
        }

        public IServiceBrand serviceBrand => _serviceBrand.Value;

        public IServiceConsumable serviceConsumable => _serviceConsumable.Value;

        public IServiceInventory serviceInventory => _serviceInventory.Value;

        public IServiceMeasurementUnit serviceMeasurementUnit => _serviceMeasurementUnit.Value;

        public IServiceModel serviceModel => _serviceModel.Value;

        public IServiceProduct serviceProduct => _serviceProduct.Value;

        public IServiceProductType serviceProductType => _serviceProductType.Value;

        public IServiceStockChange serviceStockChange => _serviceStockChange.Value;

        public IServiceSupplier serviceSupplier => _serviceSupplier.Value;

        public IServiceWarehouse serviceWarehouse => _serviceWarehouse.Value;
    }
}
