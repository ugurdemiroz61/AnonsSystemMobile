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
    internal class BelediyeSeed : IEntityTypeConfiguration<Belediye>
    {
        public void Configure(EntityTypeBuilder<Belediye> builder)
        {
            builder.HasData(new Belediye { Id = 1, BelediyeAdi = "Deneme", CreatedDate = DateTime.Now, CreateUser = ""});
        }
    }
}
