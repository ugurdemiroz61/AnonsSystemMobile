using Anons.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Seeds
{
    internal class DuyuruTipSeed : IEntityTypeConfiguration<DuyuruTip>
    {
        public void Configure(EntityTypeBuilder<DuyuruTip> builder)
        {
            builder.HasData(
                new DuyuruTip() { Id = 1, DuyuruTipAdi = "Hizmet kesintisi", CreatedDate = DateTime.Now , CreateUser = "" },
                new DuyuruTip() { Id = 2, DuyuruTipAdi = "Ölüm ilanı", CreatedDate = DateTime.Now , CreateUser =""},
                new DuyuruTip() { Id = 3, DuyuruTipAdi = "Bulunan eşya ilanı", CreatedDate = DateTime.Now , CreateUser = "" },
                new DuyuruTip() { Id = 4, DuyuruTipAdi = "Kan aranıyor", CreatedDate = DateTime.Now , CreateUser = "" }
                );
        }
    }
}
