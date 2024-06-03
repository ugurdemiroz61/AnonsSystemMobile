using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {


        public UserRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<RecordResultDto<List<UserAppDto>, NoContentDto>> getUsersAsync(PagingFilterDto<UserFiltrelemeDto> paging)
        {

            var query = (from u in _context.Users
                         select u);
            if (!string.IsNullOrEmpty(paging.Filtre.RoleName))
            {
                query = query.Where(s => (from ur in _context.UserRoles
                                          join r in _context.RoleApps on ur.RoleId equals r.Id
                                          where ur.UserId == s.Id && r.Name == paging.Filtre.RoleName
                                          select ur).Any());
            }
            if (paging.Filtre.BelediyeId > 0)
            {
                query = query.Where(s => s.BelediyeId == paging.Filtre.BelediyeId);
            }
            if (!string.IsNullOrEmpty(paging.Filtre.UserName))
            {
                query = query.Where(s => s.UserName == paging.Filtre.UserName);
            }
            if (!string.IsNullOrEmpty(paging.Filtre.Name))
            {
                query = query.Where(s => s.Name.Contains(paging.Filtre.Name));
            }
            if (!string.IsNullOrEmpty(paging.Filtre.Surname))
            {
                query = query.Where(s => s.Surname.Contains(paging.Filtre.Surname));
            }
            if (!string.IsNullOrEmpty(paging.Filtre.Email))
            {
                query = query.Where(s => s.Email.StartsWith(paging.Filtre.Email));
            }

            int rowCount = query.Count();

            List<UserAppDto> records = await query.Select(s => new UserAppDto()
            {
                Id = s.Id,
                Name = s.Name,
                Surname = s.Surname,
                UserName = s.UserName
            }).Skip(paging.Skip).Take(paging.Limit).ToListAsync();
            RecordResultDto<List<UserAppDto>, NoContentDto> recordResultDto = RecordResultDto<List<UserAppDto>, NoContentDto>.fill(records, rowCount);
            return recordResultDto;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.RoleApps.ToListAsync();
        }
        public async Task<List<MenuDto>> GetUserMenusAsync(string UserName)
        {
            return await (from u in _context.Users
                          join ur in _context.UserRoles on u.Id equals ur.UserId
                          join rm in _context.RoleMenus on ur.RoleId equals rm.RoleId
                          join m in _context.Menus on rm.MenuId equals m.Id
                          where u.UserName == UserName
                          select new MenuDto()
                          {

                              Id = m.Id,
                              MenuCode = m.MenuCode,
                              MenuName = m.MenuName,
                              MenuUrl = m.MenuUrl,
                              TopMenuCode = m.TopMenuCode
                          }).Distinct().ToListAsync();
        }

        public async Task<List<RoleDto>> GetUserRolesAsync(string userName)
        {
            return await (from u in _context.Users
                          join ur in _context.UserRoles on u.Id equals ur.UserId
                          join r in _context.Roles on ur.RoleId equals r.Id
                          where u.UserName == userName
                          select new RoleDto()
                          {
                              Id = r.Id,
                              Name = r.Name
                          }).ToListAsync();
        }
    }
}
