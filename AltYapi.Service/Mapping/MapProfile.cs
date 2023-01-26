﻿using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Models.ModelsMongo;
using AutoMapper;

namespace AltYapi.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Person, CreatePersonDto>().ReverseMap();
            CreateMap<People, CreatePersonDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            //Reversemap yapmamıza gerek yok.
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Category, CategoryWithProductsDto>();


        }
    }
}
