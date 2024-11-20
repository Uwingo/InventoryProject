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
    public class ServiceMeasurementUnit : IServiceMeasurementUnit
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceMeasurementUnit(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateMeasurementUnit(MeasurementUnitDto measurementUnitDto)
        {
            var unit = _mapper.Map<MeasurementUnit>(measurementUnitDto);
            await _repository.MeasurementUnit.Create(unit);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteMeasurementUnit(int id)
        {
            var unit = _repository.MeasurementUnit.GetById(id).Result;
            unit.IsDeleted = true;
            if (unit != null)
            {
                await _repository.MeasurementUnit.Delete(unit);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<MeasurementUnitDto> GetMeasurementUnitById(int id)
        {
            var unit = await _repository.MeasurementUnit.GetById(id);
            return _mapper.Map<MeasurementUnitDto>(unit);
        }

        public async Task<List<MeasurementUnitDto>> GetMeasurementUnitList()
        {
            var units = _repository.MeasurementUnit.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<MeasurementUnitDto>>(units);
        }

        public async Task UpdateMeasurementUnit(MeasurementUnitDto measurementUnitDto)
        {
            var unit = await _repository.MeasurementUnit.GetById(measurementUnitDto.MeasurementUnitId);
            if (unit.IsDeleted == false)
            {
                unit.MeasurementUnitName = measurementUnitDto.MeasurementUnitName;
                await _repository.MeasurementUnit.Update(unit);
                await _repository.SaveChangesAsync();
            }

        }
    }
}
