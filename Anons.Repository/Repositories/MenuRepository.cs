using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Repositories
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<RoleMenuDto>> GetRoleMenuAsync(string RoleId, int MenuId)
        {
            var query = (from rm in _context.RoleMenus
                         join r in _context.Roles on rm.RoleId equals r.Id
                         join m in _context.Menus on rm.MenuId equals m.Id
                         select new RoleMenuDto()
                         {
                             Id = rm.Id,
                             MenuId = rm.MenuId,
                             RoleId = rm.RoleId,
                             MenuName = m.MenuName,
                             RoleName = r.Name
                         }).AsQueryable();
            if (!string.IsNullOrEmpty(RoleId))
            {
                query = query.Where(s => s.RoleId == RoleId);
            }
            if (MenuId > 0)
            {
                query = query.Where(s => s.MenuId == MenuId);
            }
            return await query.ToListAsync();
        }
        public async Task<RoleMenuDto> GetRoleMenuByIdAsync(int Id)
        {
            return await (from rm in _context.RoleMenus
                          join r in _context.Roles on rm.RoleId equals r.Id
                          join m in _context.Menus on rm.MenuId equals m.Id
                          where rm.Id == Id
                          select new RoleMenuDto()
                          {
                              Id = rm.Id,
                              MenuId = rm.MenuId,
                              RoleId = rm.RoleId,
                              MenuName = m.MenuName,
                              RoleName = r.Name
                          }).FirstOrDefaultAsync();
        }
        public async Task<RoleMenu> AddRoleMenuAsync(RoleMenu roleMenu)
        {
            await _context.RoleMenus.AddAsync(roleMenu);
            await _context.SaveChangesAsync();
            return roleMenu;
        }
        public async Task RemoveRoleMenuAsync(int Id)
        {
            RoleMenu roleMenu = await _context.RoleMenus.FirstOrDefaultAsync(s => s.Id == Id);
            if (roleMenu != null)
            {
                _context.RoleMenus.Remove(roleMenu);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> MenuKoduVarmiAsync(int MenuCode, int MenuId)
        {
            var query = _context.Menus.Where(s => s.MenuCode == MenuCode);
            if (MenuId > 0)
            {
                query = query.Where(s => s.Id != MenuId);
            }
            return await query.AnyAsync();
        }
        public async Task<bool> AltMenuKoduVarmiAsync(int MenuCode)
        {
            return await _context.Menus.Where(s => s.TopMenuCode == MenuCode).AnyAsync();
        }
    }
}
