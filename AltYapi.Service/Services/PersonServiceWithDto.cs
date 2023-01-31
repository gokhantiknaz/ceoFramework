using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Repositories;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AutoMapper;

namespace AltYapi.Service.Services
{
    public class PersonServiceWithDto : ServiceWithDto<Person, CreatePersonDto>,IPersonServicesWithDto
    {


        private readonly IGenericRepository<Person> _repository;
     
        public PersonServiceWithDto(IGenericRepository<Person> repository,IUnitOfWork unitOfWork, IMapper mapper) :base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        //public async Task<CustomResponseDto<CreatePersonDto>> AddAsync(ProductCreateDto dto)
        //{
        //    var newEntity = _mapper.Map<Person>(dto);
        //    await _repository.AddAsync(newEntity);
        //    await _unitOfWork.CommitAsync();
        //    var newDto = _mapper.Map<CreatePersonDto>(newEntity);
        //    return CustomResponseDto<CreatePersonDto>.Success(StatusCodes.Status200OK, newDto);
        //}

        //public async Task<CustomResponseDto<IEnumerable<CreatePersonDto>>> AddRangeAsync(IEnumerable<ProductCreateDto> dto)
        //{
        //    var newEntities = _mapper.Map<IEnumerable<Person>>(dto);
        //    await _repository.AddRangeAsync(newEntities);
        //    await _unitOfWork.CommitAsync();
        //    var newDtos = _mapper.Map<IEnumerable<CreatePersonDto>>(newEntities);
        //    return CustomResponseDto<IEnumerable<CreatePersonDto>>.Success(StatusCodes.Status200OK, newDtos);
        //}

       

        //public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        //{
        //    var entity = _mapper.Map<Person>(dto);
        //    _repository.Update(entity);
        //    await _unitOfWork.CommitAsync();
        //    return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        //}
    }
}
