using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anons.API.Controllers
{

    public class MenuController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var menus = await _menuService.GetAllAsync();
            var menusDtos = _mapper.Map<List<MenuDto>>(menus.ToList());
            return CreateActionResult(CustomResponseDto<List<MenuDto>>.Success(StatusCodes.Status200OK, menusDtos));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var menus = await _menuService.GetByIdAsync(id);
            var menusDto = _mapper.Map<MenuDto>(menus);
            return CreateActionResult(CustomResponseDto<MenuDto>.Success(StatusCodes.Status200OK, menusDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddMenu(MenuDto menuDto)
        {
            menuDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _menuService.AddMenuAsync(menuDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(MenuDto menuDto)
        {
            menuDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _menuService.UpdateMenuAsync(menuDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _menuService.RemoveMenuAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoleMenu(RoleMenuIdsDto roleMenuIdsDto)
        {
            return CreateActionResult(await _menuService.GetRoleMenuAsync(roleMenuIdsDto.RoleId, roleMenuIdsDto.MenuId));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoleMenuById(int id)
        {
            return CreateActionResult(await _menuService.GetRoleMenuByIdAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoleMenu(AddRoleMenuIdsDto roleMenuIdsDto)
        {
            roleMenuIdsDto.CurrentUserName = HttpContext.User.Identity.Name;
            return CreateActionResult(await _menuService.AddRoleMenuAsync(roleMenuIdsDto));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveRoleMenu(int id)
        {
            return CreateActionResult(await _menuService.RemoveRoleMenuAsync(id));
        }
    }
}
