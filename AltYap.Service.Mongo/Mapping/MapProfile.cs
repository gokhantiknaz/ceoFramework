using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AutoMapper;


namespace AltYapi.Service.Mongo.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<People, CreatePersonDto>().ReverseMap();
        }
    }
}
