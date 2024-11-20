using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceStockChange
    {
        Task<IQueryable<object>> GetStockChangeList();
        Task<StockChangeDto> GetStockChangeById(int id);
        Task CreateStockChange(StockChangeDto stockChangeDto);
        Task UpdateStockChange(StockChangeDto stockChangeDto);
        Task DeleteStockChange(int id);
    }
}
