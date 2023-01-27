using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AutoMapper;

namespace AltYapi.Service.Mongo.Service
{
 
    public class PersonServiceMongo : ServiceMongo<People, CreatePersonDto>, IPersonServicesWithDto
    {


        private readonly IMongoRepository<People> _repository;

        public PersonServiceMongo(IMongoRepository<People> repository, IMapper mapper) : base(repository,mapper)
        {
            _repository = repository;
        }
    }
}
