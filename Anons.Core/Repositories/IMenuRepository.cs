using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Repositories
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        Task<List<RoleMenuDto>> GetRoleMenuAsync(string RoleId, int MenuId);
        Task<RoleMenuDto> GetRoleMenuByIdAsync(int Id);
        Task<RoleMenu> AddRoleMenuAsync(RoleMenu roleMenu);
        Task RemoveRoleMenuAsync(int Id);
        Task<bool> MenuKoduVarmiAsync(int MenuCode,int MenuId);
        Task<bool> AltMenuKoduVarmiAsync(int MenuCode);
    }
}
