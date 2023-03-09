using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCloud.Infrastructure.DataBase.Constants
{
    /// <summary>
    /// 数据库表常量
    /// </summary>
    public static class TableConsts
    {
        /// <summary>
        /// 数据库单元
        /// </summary>
        public const string DbSchema = "gold_work";

        /// <summary>
        /// 表名前缀
        /// </summary>
        public const string DbTablePrefix = "gm";

        /// <summary>
        /// Api 资源
        /// </summary>
        public const string ApiResource = "gd_api_resource";

        /// <summary>
        /// Api Scope
        /// </summary>
        public const string ApiScope = "gd_api_scope";

        /// <summary>
        /// 菜单表
        /// </summary>
        public const string MenuTable = "gm_menu";

        /// <summary>
        /// 权限表
        /// </summary>
        public const string PermissionTable = "gm_permission";

        /// <summary>
        /// 权限系统
        /// </summary>
        public const string SystemTable = "gm_system";

        /// <summary>
        /// 菜单权限关联表
        /// </summary>
        public const string MenuPermissionAssociation = "gm_menu_permission_association";

        /// <summary>
        /// 角色权限关联表
        /// </summary>
        public const string RolePermissionAssociation = "gm_role_permission_association";
    }
}
