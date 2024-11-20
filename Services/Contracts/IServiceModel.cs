using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceModel
    {
        Task<List<ModelDto>> GetModelList();
        Task<ModelDto> GetModelById(int id);
        Task CreateModel(ModelDto modelDto);
        Task UpdateModel(ModelDto modelDto);
        Task DeleteModel(int id);
    }
}
