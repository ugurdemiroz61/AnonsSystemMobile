using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Services
{
    public interface IMenuService : IService<Menu>
    {
        Task<CustomResponseDto<List<RoleMenuDto>>> GetRoleMenuAsync(string RoleId, int MenuId);
        Task<CustomResponseDto<RoleMenuDto>> GetRoleMenuByIdAsync(int Id);
        Task<CustomResponseDto<AddRoleMenuIdsDto>> AddRoleMenuAsync(AddRoleMenuIdsDto roleMenuDto);
        Task<CustomResponseDto<NoContentDto>> RemoveRoleMenuAsync(int Id);
        Task<CustomResponseDto<MenuDto>> AddMenuAsync(MenuDto menu);
        Task<CustomResponseDto<NoContentDto>> UpdateMenuAsync(MenuDto menu);
        Task<CustomResponseDto<NoContentDto>> RemoveMenuAsync(int id);
    }
}
