using Anons.Core.DTOs;
using Anons.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<RecordResultDto<List<UserAppDto>, NoContentDto>> getUsersAsync(PagingFilterDto<UserFiltrelemeDto> paging);
        Task<List<Role>> GetRolesAsync();
        Task<List<MenuDto>> GetUserMenusAsync(string UserName);
        Task<List<RoleDto>> GetUserRolesAsync(string userName);
    }
}
