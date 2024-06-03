using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Repositories
{
    public class DuyuruRepository : GenericRepository<Duyuru>, IDuyuruRepository
    {
        public DuyuruRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<RecordResultDto<List<DuyuruResultDto>, NoContentDto>> GetDuyurularAsync(PagingFilterDto<DuyuruFiltrelemeDto> paging)
        {
            var query = (from d in _context.Duyurular
                         select d);

            if (paging.Filtre.BelediyeId > 0)
            {
                query = query.Where(s => s.BelediyeId == paging.Filtre.BelediyeId);
            }
            if (paging.Filtre.IlkTarih != null)
            {
                query = query.Where(s => s.DuyuruTarihi >= paging.Filtre.IlkTarih);
            }
            if (paging.Filtre.SonTarih != null)
            {
                query = query.Where(s => s.DuyuruTarihi <= paging.Filtre.SonTarih);
            }
            if (paging.Filtre.DuyuruTipId > 0)
            {
                query = query.Where(s => s.DuyuruTipId == paging.Filtre.DuyuruTipId);
            }

            int rowCount =  query.Count();

            List<DuyuruResultDto> records = await query.Select(s => new DuyuruResultDto()
            {
                Id = s.Id,
                DuyuruIcerik = s.DuyuruIcerik,
                DuyuruTarihi = s.DuyuruTarihi,
                DuyuruTipId = s.DuyuruTipId,
                DuyuruTipAdi = s.DuyuruTip.DuyuruTipAdi,
                BelediyeAdi = s.Belediye.BelediyeAdi,
                BelediyeId = s.BelediyeId
            }).OrderByDescending(s => s.DuyuruTarihi).Skip(paging.Skip).Take(paging.Limit).ToListAsync();

            return  RecordResultDto<List<DuyuruResultDto>, NoContentDto>.fill(records, rowCount);
        }
    }
}
