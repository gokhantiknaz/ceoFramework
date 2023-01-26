﻿using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Core.UnitOfWorks;
using AltYapi.Repository.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Service.Services
{
  
    public class ServiceWithDtoMongo<Entity, Dto> : IServiceWithDto<Entity, Dto> where Entity : Document where Dto : class
    {
        private readonly IMongoRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public ServiceWithDtoMongo(IMongoRepository<Entity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
        {
            var newEntity = _mapper.Map<Entity>(dto);
            await _repository.InsertOneAsync(newEntity);

            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<Dto>(newEntity);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<Entity>>(dtos);
            await _repository.InsertManyAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<IEnumerable<Dto>>(newEntities);
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities =  _repository.AsQueryable().ToList();
            var dtos = _mapper.Map<IEnumerable<Dto>>( entities);
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto entity)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<NoContentDto>> UpdateRangeAsync(IEnumerable<Dto> dtos)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
