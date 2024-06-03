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
    internal class RoleMenuConfiguration : IEntityTypeConfiguration<RoleMenu>
    {
        public void Configure(EntityTypeBuilder<RoleMenu> builder)
        {
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.RoleId).IsRequired();

            builder.Property(s => s.MenuId).IsRequired();

            builder.Property(s => s.CreatedDate).IsRequired();

            builder.Property(s => s.CreateUser).IsRequired().HasMaxLength(100);

            builder.Property(s => s.UpdateUser).HasMaxLength(100);
        }
    }
}
