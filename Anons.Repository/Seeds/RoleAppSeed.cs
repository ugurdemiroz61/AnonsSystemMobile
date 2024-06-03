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
    internal class RoleAppSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Name = "Admin", NormalizedName = "ADMIN" , CreatedDate = DateTime.Now, CreateUser  = "" },
                new Role { Name = "Belediye", NormalizedName = "BELEDIYE" , CreatedDate = DateTime.Now , CreateUser = "" },
                new Role { Name = "Vatandas", NormalizedName = "VATANDAS" , CreatedDate = DateTime.Now , CreateUser = "" });
        }
    }
}
