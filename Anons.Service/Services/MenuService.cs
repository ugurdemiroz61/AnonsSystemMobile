using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Anons.Core.Services;
using Anons.Core.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anons.Service.Services
{

    public class MenuService : Service<Menu>, IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Menu> repository, IUnitOfWork unitOfWork, IMenuRepository menuRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponseDto<List<RoleMenuDto>>> GetRoleMenuAsync(string RoleId, int MenuId)
        {
            return CustomResponseDto<List<RoleMenuDto>>.Success(StatusCodes.Status200OK, await _menuRepository.GetRoleMenuAsync(RoleId, MenuId));
        }

        public async Task<CustomResponseDto<RoleMenuDto>> GetRoleMenuByIdAsync(int Id)
        {
            return CustomResponseDto<RoleMenuDto>.Success(StatusCodes.Status200OK, await _menuRepository.GetRoleMenuByIdAsync(Id));
        }
        public async Task<CustomResponseDto<AddRoleMenuIdsDto>> AddRoleMenuAsync(AddRoleMenuIdsDto roleMenuDto)
        {
            RoleMenu roleMenu = _mapper.Map<RoleMenu>(roleMenuDto);
            roleMenu.Id = 0;
            roleMenu.CreateUser = roleMenuDto.CurrentUserName;
            return CustomResponseDto<AddRoleMenuIdsDto>.Success(StatusCodes.Status201Created, _mapper.Map<AddRoleMenuIdsDto>(await _menuRepository.AddRoleMenuAsync(roleMenu)));
        }
        public async Task<CustomResponseDto<NoContentDto>> RemoveRoleMenuAsync(int Id)
        {
            await _menuRepository.RemoveRoleMenuAsync(Id);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<MenuDto>> AddMenuAsync(MenuDto menuDto)
        {
            Menu menu = _mapper.Map<Menu>(menuDto);
            menu.CreateUser = menuDto.CurrentUserName;
            if (menu.MenuCode == menu.TopMenuCode)
            {
                return CustomResponseDto<MenuDto>.Fail(StatusCodes.Status406NotAcceptable, "Menünün üst menüsü kendisi olamaz!");
            }
            if (await _menuRepository.MenuKoduVarmiAsync(menu.MenuCode, menu.Id))
            {
                return CustomResponseDto<MenuDto>.Fail(StatusCodes.Status406NotAcceptable, "kullanılmamış bir menü kodu verilmelidir!");
            }
            if (menu.TopMenuCode > 0)
            {
                if (!await _menuRepository.MenuKoduVarmiAsync(menu.TopMenuCode, menu.Id))
                {
                    return CustomResponseDto<MenuDto>.Fail(StatusCodes.Status406NotAcceptable, "Üst menü kodu geçersiz!");
                }
            }
            return CustomResponseDto<MenuDto>.Success(StatusCodes.Status200OK, _mapper.Map<MenuDto>(await AddAsync(menu)));
        }
        public async Task<CustomResponseDto<NoContentDto>> UpdateMenuAsync(MenuDto menuDto)
        {
            Menu UpdatingMenu = await GetByIdAsync(menuDto.Id);
            if (UpdatingMenu == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Menü Bulunamadı!");
            }
            UpdatingMenu.UpdateUser = menuDto.CurrentUserName;
            UpdatingMenu.Id = menuDto.Id;
            UpdatingMenu.MenuCode = menuDto.MenuCode;
            UpdatingMenu.MenuName = menuDto.MenuName;
            UpdatingMenu.MenuUrl = menuDto.MenuUrl;
            UpdatingMenu.TopMenuCode = menuDto.TopMenuCode;

            if (UpdatingMenu.Id <= 0)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Menu id geçersiz!");
            }
            if (UpdatingMenu.MenuCode == UpdatingMenu.TopMenuCode)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Menünün üst menüsü kendisi olamaz!");
            }
            if (await _menuRepository.MenuKoduVarmiAsync(UpdatingMenu.MenuCode, UpdatingMenu.Id))
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Kullanılmamış bir menü kodu verilmelidir!");
            }
            if (UpdatingMenu.TopMenuCode > 0)
            {
                if (!await _menuRepository.MenuKoduVarmiAsync(UpdatingMenu.TopMenuCode, 0))
                {
                    return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Üst menü kodu geçersiz!");
                }
            }
            await UpdateAsync(UpdatingMenu);
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<CustomResponseDto<NoContentDto>> RemoveMenuAsync(int id)
        {
            Menu deletingMenu = await GetByIdAsync(id);

            if (deletingMenu != null)
            {
                if (await _menuRepository.AltMenuKoduVarmiAsync(deletingMenu.MenuCode))
                {
                    return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status406NotAcceptable, "Silmek istediğiniz menünün alt menüsü mevcut silinemez!");
                }
                await RemoveAsync(deletingMenu);
            }
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
