using Anons.Core.DTOs;
using Anons.Core.Entities;
using Anons.Core.Repositories;
using Anons.Core.Services;
using Anons.Repository.Repositories;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Anons.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IBelediyeService _belediyeService;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IBelediyeService belediyeService, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _belediyeService = belediyeService;
            _userRepository = userRepository;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.UserName.ToUpper();
            user.CreatedDate = DateTime.Now;
            Belediye belediye = await _belediyeService.GetByIdAsync(user.BelediyeId);
            if (belediye == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(StatusCodes.Status404NotFound, "Seçilen belediye bulunamadı");
            }

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<UserAppDto>.Fail(StatusCodes.Status400BadRequest, errors);
            }
            await _userManager.AddToRoleAsync(user, "Vatandas");

            return CustomResponseDto<UserAppDto>.Success(StatusCodes.Status200OK, _mapper.Map<UserAppDto>(user));
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var currentUser = await _userManager.FindByNameAsync(updateUserDto.CurrentUserName);
            bool islemYapanAdminMi = await _userManager.IsInRoleAsync(currentUser, "Admin");

            var updatingUser = await _userManager.FindByNameAsync(updateUserDto.UserName);

            updatingUser.Surname = updateUserDto.Surname;
            updatingUser.Name = updateUserDto.Name;
            bool GuncellenecekBelediyeUserMi = await _userManager.IsInRoleAsync(updatingUser, "Belediye");
            if (GuncellenecekBelediyeUserMi)
            {
                if (islemYapanAdminMi)
                {
                    updatingUser.BelediyeId = updateUserDto.BelediyeId;
                }
                else
                {

                }
            }
            else
            {
                updatingUser.BelediyeId = updateUserDto.BelediyeId;
            }
            Belediye belediye = await _belediyeService.GetByIdAsync(updatingUser.BelediyeId);
            if (belediye == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, "Seçilen belediye bulunamadı");
            }


            updatingUser.UpdateUser = updateUserDto.CurrentUserName;
            updatingUser.UpdatedDate = DateTime.Now;
            var result = await _userManager.UpdateAsync(updatingUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors);
            }
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }



        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(StatusCodes.Status404NotFound, "UserName not found");
            }

            return CustomResponseDto<UserAppDto>.Success(StatusCodes.Status200OK, _mapper.Map<UserAppDto>(user));
        }

        public async Task<CustomResponseDto<NoContentDto>> CreateUserRolesAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors);
            }
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveUserRolesAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors);
            }
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<CustomResponseDto<NoContentDto>> ChangePasswordAsync(ChangePassordDto dto)
        {
            var currentUser = await _userManager.FindByNameAsync(dto.CurrentUserName);
            bool islemYapanAdminMi = await _userManager.IsInRoleAsync(currentUser, "Admin");
            if (islemYapanAdminMi || dto.CurrentUserName == dto.UserName)
            {
                var passwordChangingUser = await _userManager.FindByNameAsync(dto.UserName);

                var result = await _userManager.ChangePasswordAsync(passwordChangingUser, dto.CurrentPassword, dto.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();

                    return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors);
                }
                else
                {
                    passwordChangingUser.UpdateUser = dto.CurrentUserName;
                    passwordChangingUser.UpdatedDate = DateTime.Now;
                    await _userManager.UpdateAsync(passwordChangingUser);
                }
            }
            else
            {
                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status403Forbidden, "Bu kullanıcın şifresini değiştirme yetkiniz yoktur!");
            }
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<CustomResponseDto<NoContentDto>> ResetPasswordAsync(ResetPasswordDto dto)
        {

            var passwordChangingUser = await _userManager.FindByNameAsync(dto.UserName);
            string token = await _userManager.GeneratePasswordResetTokenAsync(passwordChangingUser);
            var result = await _userManager.ResetPasswordAsync(passwordChangingUser, token, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status400BadRequest, errors);
            }
            else
            {
                passwordChangingUser.UpdateUser = dto.CurrentUserName;
                passwordChangingUser.UpdatedDate = DateTime.Now;
                await _userManager.UpdateAsync(passwordChangingUser);

            }

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<RecordResultDto<List<UserAppDto>, NoContentDto>>> GetUsersAsync(PagingFilterDto<UserFiltrelemeDto> paging)
        {
            if (paging == null || paging.Filtre == null)
                return CustomResponseDto<RecordResultDto<List<UserAppDto>, NoContentDto>>.Fail(StatusCodes.Status400BadRequest, "Hatalı istek");

            return CustomResponseDto<RecordResultDto<List<UserAppDto>, NoContentDto>>.Success(StatusCodes.Status200OK, await _userRepository.getUsersAsync(paging));
        }

        public async Task<CustomResponseDto<List<RoleDto>>> GetRolesAsync()
        {
            List<Role> Roles = await _userRepository.GetRolesAsync();
            List<RoleDto> roleDtos = _mapper.Map<List<RoleDto>>(Roles);
            return CustomResponseDto<List<RoleDto>>.Success(StatusCodes.Status200OK, roleDtos);
        }
        public async Task<CustomResponseDto<List<MenuDto>>> GetUserMenusAsync(string UserName)
        {
            List<MenuDto> menuDtos = await _userRepository.GetUserMenusAsync(UserName);
            return CustomResponseDto<List<MenuDto>>.Success(StatusCodes.Status200OK, menuDtos);
        }

        public async Task<CustomResponseDto<List<RoleDto>>> GetUserRolesAsync(string userName)
        {
            List<RoleDto> roleDtos = await _userRepository.GetUserRolesAsync(userName);
            return CustomResponseDto<List<RoleDto>>.Success(StatusCodes.Status200OK, roleDtos);
        }
    }
}
