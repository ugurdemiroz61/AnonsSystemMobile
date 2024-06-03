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
    internal class UserPermisionConfiguration : IEntityTypeConfiguration<UserPermision>
    {
        public void Configure(EntityTypeBuilder<UserPermision> builder)
        {
            builder.Property(s => s.Id).UseIdentityColumn();

            builder.Property(s => s.UserId).IsRequired();

            builder.Property(s => s.PermisionId).IsRequired();

            builder.Property(s => s.CreatedDate).IsRequired();

            builder.Property(s => s.CreateUser).IsRequired().HasMaxLength(100);

            builder.Property(s => s.UpdateUser).HasMaxLength(100);
        }
    }
}
