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
    public class ServiceProduct : IServiceProduct
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceProduct(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.Product.Create(product);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = _repository.Product.GetById(id).Result;
            product.IsDeleted = true;
            if (product != null)
            {
                await _repository.Product.Delete(product);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _repository.Product.GetById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetProductList()
        {
            var products = _repository.Product.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var product = await _repository.Product.GetById(productDto.ProductId);
            if (product is not null)
            {
                product.Barcode = productDto.Barcode;
                product.PurchaseNumber = productDto.PurchaseNumber;
                product.StockNumber = productDto.StockNumber;
                product.SupplierId_FK = productDto.SupplierId_FK;              
                product.IsConsumables = productDto.IsConsumables;
                product.ModelId_FK = productDto.ModelId_FK;
                product.ProductName = productDto.ProductName;
                product.ProductTypeId_FK = productDto.ProductTypeId_FK;
                product.Status = productDto.Status;
                product.Count = productDto.Count;
                await _repository.Product.Update(product);
                await _repository.SaveChangesAsync();
            }

        }
    }
}
