using Anons.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Configurations
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.MenuCode).IsRequired();

            builder.Property(s => s.MenuName).IsRequired().HasMaxLength(50);

            builder.Property(s => s.TopMenuCode).IsRequired();

            builder.Property(s => s.MenuUrl).HasMaxLength(100);

            builder.Property(s => s.CreatedDate).IsRequired();

            builder.Property(s => s.CreateUser).IsRequired().HasMaxLength(100);

            builder.Property(s => s.UpdateUser).HasMaxLength(100);

        }
    }
}
