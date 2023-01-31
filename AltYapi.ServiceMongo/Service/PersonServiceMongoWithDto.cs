using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AltYapi.RepositoryMongo.UnitOfWorks;
using AutoMapper;


namespace AltYapi.ServiceMongo.Service
{
    public class PersonServiceMongoWithDto : ServiceMongo<People, CreatePersonDto>, IPersonServicesWithDto
    {


        private readonly IGenericRepositoryMongo<People> _repository;

        private readonly RepositoryMongo.UnitOfWorks.IUnitOfWork _unitOfWork;

        public PersonServiceMongoWithDto(IGenericRepositoryMongo<People> repository, RepositoryMongo.UnitOfWorks.IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _unitOfWork=unitOfWork;
        }
    }
}
