using Anons.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Seeds
{
    internal class UserAppSeed : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
           // UserApp adminUser = new UserApp { Id = "1", Name = "Admin", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", NormalizedUserName = "ADMIN", Surname = "admin", UserName = "admin"  };
          //  builder.HasData(adminUser);
        }
    }

}
