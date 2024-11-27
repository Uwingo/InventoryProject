using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<WarehouseDto,Warehouse>().ReverseMap();
            CreateMap<SupplierDto, Supplier>().ReverseMap();
            CreateMap<StockChangeDto, StockChange>().ReverseMap();
            CreateMap<ProductTypeDto,ProductType>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ModelDto, Model>().ReverseMap();
            CreateMap<MeasurementUnitDto, MeasurementUnit>().ReverseMap();
            CreateMap<InventoryDto, InventoryStock>().ReverseMap();
            CreateMap<ConsumableDto, Consumable>().ReverseMap();
            CreateMap<BrandDto, Brand>().ReverseMap(); 
            CreateMap<Brand, ConsumableDto>().ReverseMap();
            CreateMap<Product, ConsumableDto>().ReverseMap();
            CreateMap<User, UserForRegistrationDTO>().ReverseMap();
        }
    }
}
