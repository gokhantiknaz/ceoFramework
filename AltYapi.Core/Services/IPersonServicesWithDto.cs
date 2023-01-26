using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AltYapi.Core.Services
{

    public interface IPersonServicesWithDto 
    {
        Task<CustomResponseDto<CreatePersonDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<CreatePersonDto>>> GetAllAsync();

        Task<CustomResponseDto<CreatePersonDto>> AddAsync(CreatePersonDto CreatePersonDto);
        Task<CustomResponseDto<IEnumerable<CreatePersonDto>>> AddRangeAsync(IEnumerable<CreatePersonDto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(CreatePersonDto entity);
        Task<CustomResponseDto<NoContentDto>> UpdateRangeAsync(IEnumerable<CreatePersonDto> dtos);
        //İd ler entity e çevrilebilir şimdilik id yapıldı
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
