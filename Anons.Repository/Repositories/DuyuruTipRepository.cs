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
    public class DuyuruTipRepository : GenericRepository<DuyuruTip>, IDuyuruTipRepository
    {
        public DuyuruTipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<DuyuruTip> AddDuyuruTipAsync(DuyuruTip duyuruTip)
        {
            await _context.DuyuruTipleri.AddAsync(duyuruTip);
            await _context.SaveChangesAsync();
            return duyuruTip;
        }

        public async Task RemoveDuyuruTipAsync(int duyuruTipId)
        {
            DuyuruTip duyuruTip = await GetByIdAsync(duyuruTipId);
            if (duyuruTip != null)
            {
                _context.DuyuruTipleri.Remove(duyuruTip);
                _context.SaveChanges();
            }
        }
        public async Task<bool> DuyuruTipKullanildiMiAsync(int duyuruTipId)
        {
            return await _context.Duyurular.Where(s => s.DuyuruTipId == duyuruTipId).AnyAsync();
        }

        public async Task UpdateDuyuruTipAsync(DuyuruTip duyuruTip)
        {
            var updatingDuyuruTip = await GetByIdAsync(duyuruTip.Id);

            updatingDuyuruTip.DuyuruTipAdi = duyuruTip.DuyuruTipAdi;
            updatingDuyuruTip.UpdateUser = duyuruTip.UpdateUser;
            await _context.SaveChangesAsync();
        }
    }
}
