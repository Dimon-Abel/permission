using System;
using System.Collections.Generic;
using GoldCloud.Infrastructure.DataBase.Constants;
using GoldCloud.Infrastructure.LinqToDb.Converter;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using LinqToDB.Mapping;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.MenuTable)]
    public class Menu
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Column("id", IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [Column("path")]
        public string Path { get; set; }

        /// <summary>
        /// 上级菜单标识
        /// </summary>
        [Column("parent_id"), Nullable]
        public long? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("order")]
        public int Order { get; set; }

        /// <summary>
        /// 是否禁用    true|禁用~false|未禁用
        /// </summary>
        [Column("disabled")]
        public bool Disabled { get; set; }

        /// <summary>
        /// 是否隐藏菜单 true|隐藏~false|不隐藏
        /// </summary>
        [Column("hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        /// 归属系统
        /// </summary>
        [Column("system_id"), Nullable]
        public long SystemId { get; set; }

        /// <summary>
        /// 组件路由
        /// </summary>
        [Column("component_url")]
        public string ComponentUrl { get; set; }

        /// <summary>
        /// 菜单说明
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// meta 数据
        /// </summary>
        [Column("meta", DataType = LinqToDB.DataType.Json)]
        [ValueConverter(ConverterType = typeof(JsonValueConverter<Meta>))]
        public Meta Meta { get; set; }

        /// <summary>
        /// 无下级时，是否折叠显示
        /// </summary>
        [Column("always_show")]
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 翻译标记
        /// </summary>
        [Column("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        [Column("full_path")]
        public string FullPath { get; set; }

        /// <summary>
        /// 关联关系
        /// </summary>
        [Association(ThisKey = nameof(Id), OtherKey = nameof(MenuPermissionAssociation.MenuId))]
        public List<MenuPermissionAssociation> AssociationEntites { get; set; } = new List<MenuPermissionAssociation>();

        /// <summary>
        /// 归属系统
        /// </summary>
        [Association(ThisKey = nameof(SystemId), OtherKey = nameof(SystemEntity.Id), CanBeNull = true)]
        public SystemEntity System { get; set; }
    }
}
