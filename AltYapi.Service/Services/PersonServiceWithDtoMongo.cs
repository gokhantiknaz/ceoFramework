using AltYapi.Core.Dtos;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Service.Services
{

    //public class PersonServiceWithDto : ServiceWithDto<Person, CreatePersonDto>, IPersonServicesWithDto<CreatePersonDto>

    public class PersonServiceWithDtoMongo : ServiceWithDtoMongo<People, CreatePersonDto>, IPersonServicesWithDto
    {


        private readonly IMongoRepository<People> _repository;

        public PersonServiceWithDtoMongo(IMongoRepository<People> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        //public async Task<CustomResponseDto<CreatePersonDto>> AddAsync(CreatePersonDto dto)
        //{
        //    var newEntity = _mapper.Map<People>(dto);
        //    await _repository.InsertOneAsync(newEntity);
        //    await _unitOfWork.CommitAsync();
        //    var newDto = _mapper.Map<CreatePersonDto>(newEntity);
        //    return CustomResponseDto<CreatePersonDto>.Success(StatusCodes.Status200OK, newDto);
        //}

        //public async Task<CustomResponseDto<IEnumerable<CreatePersonDto>>> AddRangeAsync(IEnumerable<CreatePersonDto> dto)
        //{
        //    var newEntities = _mapper.Map<IEnumerable<People>>(dto);
        //    await _repository.InsertManyAsync(newEntities);
        //    await _unitOfWork.CommitAsync();
        //    var newDtos = _mapper.Map<IEnumerable<CreatePersonDto>>(newEntities);
        //    return CustomResponseDto<IEnumerable<CreatePersonDto>>.Success(StatusCodes.Status200OK, newDtos);
        //}



        //public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(CreatePersonDto dto)
        //{
        //    var entity = _mapper.Map<People>(dto);
        //    await _repository.ReplaceOneAsync(entity);
        //    await _unitOfWork.CommitAsync();
        //    return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        //}
    }
}
