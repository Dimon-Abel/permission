using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Commands.Menu
{
    /// <summary>
    /// 添加菜单
    /// </summary>
    [Command(Name = nameof(CreateMenuCommand))]
    public class CreateMenuCommand : KingMetalCommand
    {
        #region 属性

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 上级菜单标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否禁用    true|禁用~false|未禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 是否隐藏菜单 true|隐藏~false|不隐藏
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 归属系统
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 菜单路径 "/" 分隔
        /// </summary>
        public string ComponentUrl { get; set; }

        /// <summary>
        /// 菜单说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// meta 数据
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// 无下级时，是否折叠显示
        /// </summary>
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 翻译标记
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public List<ApiScopeEntity> Scopes { get; set; }

        #endregion 属性

        public CreateMenuCommand()
        {
            Scopes = new List<ApiScopeEntity>();
        }
    }
}
