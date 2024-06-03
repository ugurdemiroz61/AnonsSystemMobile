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
    internal class PermisionConfiguration : IEntityTypeConfiguration<Permision>
    {
        public void Configure(EntityTypeBuilder<Permision> builder)
        {
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.PermisionName).HasMaxLength(100).IsRequired();

            builder.Property(s => s.CreatedDate).IsRequired();

            builder.Property(s => s.CreateUser).IsRequired().HasMaxLength(100);

            builder.Property(s => s.UpdateUser).HasMaxLength(100);
        }
    }
}
