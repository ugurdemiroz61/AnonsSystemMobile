using Anons.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Core.Services
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName);
        Task<CustomResponseDto<NoContentDto>> CreateUserRolesAsync(string username, string roleName);
        Task<CustomResponseDto<NoContentDto>> RemoveUserRolesAsync(string username, string roleName);
        Task<CustomResponseDto<NoContentDto>> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task<CustomResponseDto<NoContentDto>> ChangePasswordAsync(ChangePassordDto dto);
        Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto dto);
        Task<CustomResponseDto<RecordResultDto<List<UserAppDto>, NoContentDto>>> GetUsersAsync(PagingFilterDto<UserFiltrelemeDto> paging);
        Task<CustomResponseDto<List<RoleDto>>> GetRolesAsync();
        Task<CustomResponseDto<List<MenuDto>>> GetUserMenusAsync(string UserName);
        Task<CustomResponseDto<List<RoleDto>>> GetUserRolesAsync(string userName);
    }
}
