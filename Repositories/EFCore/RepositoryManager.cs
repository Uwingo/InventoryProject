using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IRepositoryBrand> _repositoryBrand;
        private readonly Lazy<IRepositoryConsumable> _repositoryConsumable;
        private readonly Lazy<IRepositoryInventory> _repositoryInventory;
        private readonly Lazy<IRepositoryMeasurementUnit> _repositoryMeasurementUnit;
        private readonly Lazy<IRepositoryModel> _repositoryModel;
        private readonly Lazy<IRepositoryProduct> _repositoryProduct;
        private readonly Lazy<IRepositoryProductType> _repositoryProductType;
        private readonly Lazy<IRepositoryStockChange> _repositoryStockChange;
        private readonly Lazy<IRepositorySupplier> _repositorySupplier;
        private readonly Lazy<IRepositoryWarehouse> _repositoryWarehouse;
        public IRepositoryBrand Brand => _repositoryBrand.Value;

        public IRepositoryConsumable Consumable => _repositoryConsumable.Value;

        public IRepositoryInventory Inventory => _repositoryInventory.Value;

        public IRepositoryMeasurementUnit MeasurementUnit => _repositoryMeasurementUnit.Value;

        public IRepositoryModel Model => _repositoryModel.Value;

        public IRepositoryProduct Product => _repositoryProduct.Value;

        public IRepositoryProductType ProductType => _repositoryProductType.Value;

        public IRepositoryStockChange StockChange => _repositoryStockChange.Value;

        public IRepositorySupplier Supplier => _repositorySupplier.Value;

        public IRepositoryWarehouse Warehouse => _repositoryWarehouse.Value;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _repositoryBrand = new Lazy<IRepositoryBrand>(() => new RepositoryBrand(_context));
            _repositoryConsumable = new Lazy<IRepositoryConsumable>(() => new RepositoryConsumable(_context));
            _repositoryInventory = new Lazy<IRepositoryInventory>(() => new RepositoryInventory(_context));
            _repositoryMeasurementUnit = new Lazy<IRepositoryMeasurementUnit>(() => new RepositoryMeasurementUnit(_context));
            _repositoryModel = new Lazy<IRepositoryModel>(() => new RepositoryModel(_context));
            _repositoryProduct = new Lazy<IRepositoryProduct>(() => new RepositoryProduct(_context));
            _repositoryProductType = new Lazy<IRepositoryProductType>(() => new RepositoryProductType(_context));
            _repositoryStockChange = new Lazy<IRepositoryStockChange>(() => new RepositoryStockChange(_context));
            _repositorySupplier = new Lazy<IRepositorySupplier>(() => new RepositorySupplier(_context));
            _repositoryWarehouse = new Lazy<IRepositoryWarehouse>(() => new RepositoryWarehouse(_context));

        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
