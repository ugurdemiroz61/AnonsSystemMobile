using Anons.Core.DTOs;
using Anons.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Anons.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Belediye, BelediyeDto>().ReverseMap();
            CreateMap<Duyuru, DuyuruDto>().ReverseMap();
            CreateMap<DuyuruTip, DuyuruTipDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<Permision, PermisionDto>().ReverseMap();
            CreateMap<UserPermision, UserPermisionDto>().ReverseMap();

            CreateMap<User, UserAppDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<DuyuruDto, Duyuru>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleMenu,RoleMenuDto>().ReverseMap();
            CreateMap<RoleMenu, AddRoleMenuIdsDto>().ReverseMap();

        }
    }
}
