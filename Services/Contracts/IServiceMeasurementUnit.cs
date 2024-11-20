using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceMeasurementUnit
    {
        Task<List<MeasurementUnitDto>> GetMeasurementUnitList();
        Task<MeasurementUnitDto> GetMeasurementUnitById(int id);
        Task CreateMeasurementUnit(MeasurementUnitDto measurementUnitDto);
        Task UpdateMeasurementUnit(MeasurementUnitDto measurementUnitDto);
        Task DeleteMeasurementUnit(int id);
    }
}
