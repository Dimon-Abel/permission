using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB.Mapping;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    /// <summary>
    /// 菜单权限关联
    /// </summary>
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.MenuPermissionAssociation)]
    public class MenuPermissionAssociation
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Column("menu_id")]
        public long MenuId { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        [Column("permission_id")]
        public long PermissionId { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [Association(ThisKey = nameof(MenuId), OtherKey = nameof(Menu.Id), CanBeNull = true)]
        public Menu MenuEntity { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Association(ThisKey = nameof(PermissionId), OtherKey = nameof(Permission.Id), CanBeNull = true)]
        public Permission PermissionEntity { get; set; }
    }
}
