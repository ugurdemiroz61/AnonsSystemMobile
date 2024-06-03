using Anons.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(s => s.BelediyeId).IsRequired();

            builder.Property(s => s.Email).IsRequired().HasMaxLength(100);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

            builder.Property(s => s.Surname).IsRequired().HasMaxLength(100);

            builder.Property(s => s.CreatedDate).IsRequired();

            builder.Property(s => s.CreateUser).HasMaxLength(100);

            builder.Property(s => s.UpdateUser).HasMaxLength(100);

            builder.Property(s => s.UserName).IsRequired().HasMaxLength(100);
        }
    }
}
