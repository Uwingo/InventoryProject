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
    public class ServiceConsumable : IServiceConsumable
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ServiceConsumable(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateConsumable(ConsumableDto consumableDto)
        {
            var consumable = _mapper.Map<Consumable>(consumableDto);
            await _repository.Consumable.Create(consumable);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteConsumable(int id)
        {
            var consumable = _repository.Consumable.GetById(id).Result;
            consumable.IsDeleted = true;
            if (consumable != null)
            {
                await _repository.Consumable.Delete(consumable);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<ConsumableDto> GetConsumableById(int id)
        {
            var brand = await _repository.Consumable.GetById(id);
            return _mapper.Map<ConsumableDto>(brand);
        }

        public async Task<List<ConsumableDto>> GetConsumableList()
        {
            var consumables = _repository.Consumable.Read(false).Result.Where(w => w.IsDeleted == false).ToList();
            return _mapper.Map<List<ConsumableDto>>(consumables);
        }

        public async Task UpdateConsumable(ConsumableDto consumableDto)
        {
            var consumable = await _repository.Consumable.GetById(consumableDto.ConsumableId);
            if (consumable != null)
            {
                consumable.ConsumableDescription=consumableDto.ConsumableDescription;
                consumable.Count=consumableDto.Count;
                consumable.ProductId_FK=consumableDto.ProductId_FK;
                await _repository.Consumable.Update(consumable);
                await _repository.SaveChangesAsync();
            }

        }
    }
}
