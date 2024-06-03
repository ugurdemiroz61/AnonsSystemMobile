using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Repositories
{
    public interface IDuyuruRepository : IGenericRepository<Duyuru>
    {
        Task<RecordResultDto<List<DuyuruResultDto>, NoContentDto>> GetDuyurularAsync(PagingFilterDto<DuyuruFiltrelemeDto> paging);
    }
}
