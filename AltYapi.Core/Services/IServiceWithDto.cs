﻿using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Core.Services
{
    public interface IServiceWithDto<Entitiy, Dto> where Entitiy : BaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entitiy, bool>> expression);
        Task<CustomResponseDto< bool>> AnyAsync(Expression<Func<Entitiy, bool>> expression);
        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto entity);
        Task<CustomResponseDto<NoContentDto>> UpdateRangeAsync(IEnumerable<Dto> dtos);
        //İd ler entity e çevrilebilir şimdilik id yapıldı
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
