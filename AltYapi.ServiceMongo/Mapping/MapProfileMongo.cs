using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AutoMapper;


namespace AltYapi.ServiceMongo.Mapping
{
    public class MapProfileMongo : Profile
    {

        public MapProfileMongo()
        {
            CreateMap<People, CreatePersonDto>().ReverseMap();
        }
    }
}
