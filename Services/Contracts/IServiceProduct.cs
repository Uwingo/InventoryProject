using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceProduct
    {
        Task<List<ProductDto>> GetProductList();
        Task<ProductDto> GetProductById(int id);
        Task CreateProduct(ProductDto productDto);
        Task UpdateProduct(ProductDto productDto);
        Task DeleteProduct(int id);
    }
}
