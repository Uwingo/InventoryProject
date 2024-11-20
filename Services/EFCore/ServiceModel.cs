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
    public class ServiceModel:IServiceModel
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceModel(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateModel(ModelDto modelDto)
        {
            var model = _mapper.Map<Model>(modelDto);
            await _repository.Model.Create(model);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteModel(int id)
        {
            var model = _repository.Model.GetById(id).Result;
            model.IsDeleted = true;
            if (model != null)
            {
                await _repository.Model.Delete(model);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<ModelDto> GetModelById(int id)
        {
            var model= await _repository.Model.GetById(id);
            return _mapper.Map<ModelDto>(model);
        }

        public async Task<List<ModelDto>> GetModelList()
        {
            var models = _repository.Model.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<ModelDto>>(models);
        }

        public async Task UpdateModel(ModelDto modelDto)
        {
            //var model = _mapper.Map<Model>(modelDto);
            var model =  await _repository.Model.GetById(modelDto.ModelId);
            if (model.IsDeleted == false)
            {
                model.ModelName = modelDto.ModelName;
                model.BrandId_FK = modelDto.BrandId_FK;
                await _repository.Model.Update(model);
                await _repository.SaveChangesAsync();
            }
            
        }
    }
}
