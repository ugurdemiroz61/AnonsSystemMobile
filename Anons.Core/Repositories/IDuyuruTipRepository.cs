using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Repositories
{
    public interface IDuyuruTipRepository : IGenericRepository<DuyuruTip>
    {
        Task<DuyuruTip> AddDuyuruTipAsync(DuyuruTip duyuruTip);
        Task RemoveDuyuruTipAsync(int duyuruTipId);
        Task UpdateDuyuruTipAsync(DuyuruTip duyuruTip);
        Task<bool> DuyuruTipKullanildiMiAsync(int duyuruTipId);
    }
}
