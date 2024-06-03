using Anons.Core.DTOs;
using Anons.Core.Services;
using Anons.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _userService.CreateUserAsync(createUserDto));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            updateUserDto.CurrentUserName= HttpContext.User.Identity.Name;
            return CreateActionResult(await _userService.UpdateUserAsync(updateUserDto));
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUser()
        {
            return CreateActionResult(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles()
        {
            return CreateActionResult(await _userService.GetUserRolesAsync(HttpContext.User.Identity.Name));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddAdminRole/{userName}")]
        public async Task<IActionResult> AddAdminRole(string userName)
        {
            return CreateActionResult(await _userService.CreateUserRolesAsync(userName, "Admin"));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("RemoveAdminRole/{userName}")]
        public async Task<IActionResult> RemoveAdminRole(string userName)
        {
            return CreateActionResult(await _userService.RemoveUserRolesAsync(userName, "Admin"));
        }

        [Authorize(Roles = "Admin,Belediye")]
        [HttpPost("AddBelediyeRole/{userName}")]
        public async Task<IActionResult> AddBelediyeRole(string userName)
        {
            return CreateActionResult(await _userService.CreateUserRolesAsync(userName, "Belediye"));
        }

        [Authorize(Roles = "Admin,Belediye")]
        [HttpDelete("RemoveBelediyeRole/{userName}")]
        public async Task<IActionResult> RemoveBelediyeRole(string userName)
        {
            return CreateActionResult(await _userService.RemoveUserRolesAsync(userName, "Belediye"));
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePassordDto changePassordDto)
        {
            changePassordDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _userService.ChangePasswordAsync(changePassordDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassordDto)
        {
            resetPassordDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _userService.ResetPasswordAsync(resetPassordDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers(PagingFilterDto<UserFiltrelemeDto> paging)
        {
            return CreateActionResult(await _userService.GetUsersAsync(paging));
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles()
        {
            return CreateActionResult(await _userService.GetRolesAsync());
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserMenus()
        {
            return CreateActionResult(await _userService.GetUserMenusAsync(HttpContext.User.Identity.Name));
        }
        
    }
}
