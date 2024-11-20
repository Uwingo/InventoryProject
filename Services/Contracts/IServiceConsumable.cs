using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceConsumable
    {
        Task<List<ConsumableDto>> GetConsumableList();
        Task<ConsumableDto> GetConsumableById(int id);
        Task CreateConsumable(ConsumableDto consumableDto);
        Task UpdateConsumable(ConsumableDto consumableDto);
        Task DeleteConsumable(int id);
    }
}
