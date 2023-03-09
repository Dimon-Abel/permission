using AutoMapper;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Permissions.Api.Dtos;
using GoldCloud.Permissions.Api.Dtos.Common;
using GoldCloud.Permissions.Api.Dtos.Menu;
using GoldCloud.Permissions.Api.Dtos.Permission;
using GoldCloud.Domain.Interfaces.Commands.Permission;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Domain.Interfaces.Commands.Menu;
using GoldCloud.Domain.Interfaces.Commands.System;

namespace GoldCloud.Permissions.Api.Mapper
{
    /// <summary>
    /// Menu Mapper
    /// </summary>
    public class PermissionProfile : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public PermissionProfile()
        {


            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, MenuTreeNode>();
            CreateMap<MenuDto, MenuTreeNode>();
            CreateMap<MenuDto, MenuTreeNode>();
            CreateMap<CreateMenuDto, CreateMenuCommand>();
            CreateMap<UpdateMenuDto, UpdateMenuCommand>();

            CreateMap<Permission, PermissionInfo>()
                    .ForMember(x => x.Resource, map => map.MapFrom(x => x.Resource));

            CreateMap<Permission, PermissionDto>();
            CreateMap<CreatePermissionDto, CreatePermissionCommand>().ForMember(x => x.Resource, map => map.Ignore());
            CreateMap<UpdatePermissionDto, UpdatePermissionCommand>().ForMember(x => x.Resource, map => map.Ignore());

            CreateMap<Permission, PermissionTreeNode>()
                     .ForMember(x => x.Resources, map => map.MapFrom(x => x.Resource))
                     .ForMember(x => x.System, map => map.MapFrom(x => x.System))
                     .ForMember(x => x.Children, map => map.Ignore());

            CreateMap<SystemEntity, SystemInfo>();
            CreateMap<SystemEntity, SystemDto>();
            CreateMap<CreateSystemDto, CreateSystemCommand>();
            CreateMap<UpdateSystemDto, UpdateSystemCommand>();

            CreateMap<ApiResource, ApiResourceDto>();
            CreateMap<ApiScope, ApiScopeDto>();
            CreateMap<ApiScope, ApiScopeEntity>();
        }
    }
}
