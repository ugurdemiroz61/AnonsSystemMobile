using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Anons.Core.Entities;
using Anons.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Anons.Repository
{
    public class AppDbContext  : IdentityDbContext<User, IdentityRole, string>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<DuyuruTip> DuyuruTipleri { get; set; }
        public DbSet<Belediye> Belediyeler { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permision> Permisions { get; set; }
        public DbSet<User> UserApps { get; set; }
        public DbSet<Role> RoleApps { get; set; }
    //    public DbSet<UserRole> UserRoleApps { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<UserPermision> UserPermisions { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            /* modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
             {
                 Id = 1,
                 Color = "Kırmızı",
                 Height = 100,
                 Width = 200,
                 ProductId = 1


             },
             new ProductFeature()
             {
                 Id = 2,
                 Color = "Mavi",
                 Height = 300,
                 Width = 500,
                 ProductId = 2


             }


             );*/




            base.OnModelCreating(modelBuilder);
        }
    }
}
