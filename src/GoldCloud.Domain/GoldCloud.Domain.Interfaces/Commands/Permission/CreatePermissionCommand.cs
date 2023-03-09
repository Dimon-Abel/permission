using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 添加权限
    /// </summary>
    [Command(Name = nameof(CreatePermissionCommand))]
    public class CreatePermissionCommand : KingMetalCommand
    {
        #region 属性

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指令
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 上级权限标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否拥有系统权限 true|有~false|无
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 权限类型    1|菜单~2|按钮
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 权限路径 "/" 分隔
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public List<ApiScopeEntity> Resource { get; set; }

        /// <summary>
        /// 权限备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public CreatePermissionCommand()
        {
            Resource = new List<ApiScopeEntity>();
        }

        #endregion
    }
}
