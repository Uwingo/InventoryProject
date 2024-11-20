using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceProductType
    {
        Task<List<ProductTypeDto>> GetProductTypeList();
        Task<ProductTypeDto> GetProductTypeById(int id);
        Task CreateProductType(ProductTypeDto productTypeDto);
        Task UpdateProductType(ProductTypeDto productTypeDto);
        Task DeleteProductType(int id);
    }
}
