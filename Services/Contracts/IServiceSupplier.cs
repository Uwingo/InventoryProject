using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceSupplier
    {
        Task<List<SupplierDto>> GetSupplierList();
        Task<SupplierDto> GetSupplierById(int id);
        Task CreateSupplier(SupplierDto supplierDto);
        Task UpdateSupplier(SupplierDto supplierDto);
        Task DeleteSupplier(int id);
    }
}
