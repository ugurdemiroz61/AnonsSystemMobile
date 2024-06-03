using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Services
{
    public interface IBelediyeService :IService<Belediye>
    {
        Task<CustomResponseDto<List<BelediyeDto>>> GetUstBelediyelerAsync();
        Task<CustomResponseDto<List<BelediyeDto>>> GetAltBelediyelerAsync(int ustBelediyeId);
        Task<CustomResponseDto<BelediyeDto>> AddBelediyeAsync(BelediyeDto belediye);
        Task<CustomResponseDto<NoContentDto>> UpdateBelediyeAsync(BelediyeDto belediye);
        Task<CustomResponseDto<NoContentDto>> RemoveBelediyeAsync(int belediyeId);
    }
}
