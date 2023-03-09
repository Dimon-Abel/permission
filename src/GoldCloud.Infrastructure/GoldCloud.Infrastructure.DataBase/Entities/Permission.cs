using System;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB;
using LinqToDB.Mapping;
using System.Collections.Generic;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.LinqToDb.Converter;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.PermissionTable)]
    public class Permission
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        [Column("id", IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 指令
        /// </summary>
        [Column("command")]
        public string Command { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("order")]
        public int Order { get; set; }

        /// <summary>
        /// 上级权限标识
        /// </summary>
        [Column("parent_id"), Nullable]
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否拥有系统权限 true|有~false|无
        /// </summary>
        [Column("is_system")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 权限类型    1|菜单~2|按钮
        /// </summary>
        [Column("type", DataType = DataType.Int32)]
        public PermissionType Type { get; set; }

        /// <summary>
        /// 权限路径 "/" 分隔
        /// </summary>
        [Column("path")]
        public string Path { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        [Column("system_id")]
        public long SystemId { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        [Column("resource", DataType = DataType.Json)]
        [ValueConverter(ConverterType = typeof(JsonValueConverter<List<ApiScopeEntity>>))]
        public List<ApiScopeEntity> Resource { get; set; }

        /// <summary>
        /// 权限备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 角色权限关联
        /// </summary>
        [Association(ThisKey = nameof(Id), OtherKey = nameof(RolePermissionAssociation.PermissionId))]
        public List<RolePermissionAssociation> AssociationEntites { get; set; } = new List<RolePermissionAssociation>();

        /// <summary>
        /// 归属系统
        /// </summary>
        [Association(ThisKey = nameof(SystemId), OtherKey = nameof(SystemEntity.Id), CanBeNull = true)]
        public SystemEntity System { get; set; }
    }
}
