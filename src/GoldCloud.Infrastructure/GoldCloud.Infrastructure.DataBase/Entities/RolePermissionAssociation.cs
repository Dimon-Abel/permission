using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB.Mapping;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    /// <summary>
    /// 角色权限关联
    /// </summary>
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.RolePermissionAssociation)]
    public class RolePermissionAssociation
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        [Column("role_id")]
        public string RoleId { get; set; }

        /// <summary>
        /// 权限标识 
        /// </summary>
        [Column("permission_id")]
        public long PermissionId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Association(ThisKey = nameof(PermissionId), OtherKey = nameof(Permission.Id), CanBeNull = true)]
        public Permission PermissionEntity { get; set; }
    }
}
