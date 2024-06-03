using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Services
{
    public interface IDuyuruService : IService<Duyuru>
    {
        Task<CustomResponseDto<DuyuruDto>> AddDuyuruAsync(DuyuruDto duyuruDto);
        Task<CustomResponseDto<RecordResultDto<List<DuyuruResultDto>, NoContentDto>>> GetDuyurularAsync(PagingFilterDto<DuyuruFiltrelemeDto> paging);
        Task<CustomResponseDto<NoContentDto>> UpdateDuyuruAsync(DuyuruDto duyuruDto);
        Task<CustomResponseDto<NoContentDto>> RemoveDuyuruAsync(int duyuruId, string currentUserName);
        Task<CustomResponseDto<List<DuyuruTipDto>>> GetDuyuruTipleriAsync();
        Task<CustomResponseDto<DuyuruTipDto>> AddDuyuruTipAsync(DuyuruTipDto duyuruTipDto);
        Task<CustomResponseDto<NoContentDto>> UpdateDuyuruTipAsync(DuyuruTipDto duyuruTipDto);
        Task<CustomResponseDto<NoContentDto>> RemoveDuyuruTipAsync(int duyuruTipId);
        Task<CustomResponseDto<DuyuruTipDto>> GetDuyuruTipByIdAsync(int id);
    }
}
