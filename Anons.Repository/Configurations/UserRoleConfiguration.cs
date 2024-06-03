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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder.Property(s => s.CreateUser).IsRequired().HasMaxLength(100);
            builder.Property(s => s.UpdateUser).HasMaxLength(100);
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.RoleId).IsRequired();
        }
    }
}
