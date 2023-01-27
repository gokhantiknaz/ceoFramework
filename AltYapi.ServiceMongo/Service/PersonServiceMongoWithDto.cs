using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AutoMapper;


namespace AltYapi.ServiceMongo.Service
{
    public class PersonServiceMongoWithDto : ServiceMongo<People, CreatePersonDto>, IPersonServicesWithDto
    {


        private readonly IGenericRepositoryMongo<People> _repository;

        public PersonServiceMongoWithDto(IGenericRepositoryMongo<People> repository,IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }
    }
}
