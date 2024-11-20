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
    public class ServiceSupplier : IServiceSupplier
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceSupplier(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateSupplier(SupplierDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _repository.Supplier.Create(supplier);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteSupplier(int id)
        {
            var supplier = _repository.Supplier.GetById(id).Result;
            supplier.IsDeleted = true;
            if (supplier != null)
            {
                await _repository.Supplier.Delete(supplier);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<SupplierDto> GetSupplierById(int id)
        {
            var supplier = await _repository.Supplier.GetById(id);
            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<List<SupplierDto>> GetSupplierList()
        {
            var suppliers = _repository.Supplier.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<SupplierDto>>(suppliers);
        }

        public async Task UpdateSupplier(SupplierDto supplierDto)
        {
            var supplier = await _repository.Supplier.GetById(supplierDto.SupplierId);
            if (supplier != null)
            {
                supplier.SupplierName = supplierDto.SupplierName;
                supplier.ContactInfo = supplierDto.ContactInfo;
                await _repository.Supplier.Update(supplier);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
