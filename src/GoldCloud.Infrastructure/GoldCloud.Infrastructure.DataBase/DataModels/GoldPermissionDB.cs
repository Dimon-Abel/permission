using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using GoldCloud.Infrastructure.DataBase.Entities;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.DataProvider.PostgreSQL;
using LinqToDB.Mapping;

namespace GoldCloud.Infrastructure.DataBase
{
    public class GoldPermissionDB: DataConnection
    {
        /// <summary>
        /// 初始化一个 <see cref="GoldPermissionDB" /> 类型的新实例
        /// </summary>
        /// <param name="options"> 配置对象 </param>
        public GoldPermissionDB(LinqToDBConnectionOptions options) : base(options)
        {
            InitMappingSchema();
        }

        /// <summary>
        /// 初始化一个 <see cref="GoldPermissionDB" /> 类型的新实例
        /// </summary>
        /// <param name="connectionString"> 数据库连接字符串 </param>
        public GoldPermissionDB(string connectionString) : base(PostgreSQLTools.GetDataProvider(PostgreSQLVersion.v95), connectionString)
        {
            InitMappingSchema();
        }

        public void InitMappingSchema()
        {
            var _serializerOptions = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };

            #region Permission

            // Permission to json string json string to Permission
            MappingSchema.Default.SetConverter<Permission, string>((obj) =>
            {
                if (obj == null)
                    return null;
                return JsonSerializer.Serialize(obj, _serializerOptions);
            });
            MappingSchema.Default.SetConverter<string, Permission>((txt) =>
            {
                if (string.IsNullOrEmpty(txt))
                    return null;
                return JsonSerializer.Deserialize<Permission>(txt, _serializerOptions);
            });

            #endregion

        }

        /// <summary>
        /// ApiResource
        /// </summary>
        public ITable<ApiResource> ApiResources { get { return this.GetTable<ApiResource>(); } }

        /// <summary>
        /// ApiScope
        /// </summary>
        public ITable<ApiScope> ApiScopes { get { return this.GetTable<ApiScope>(); } }

        /// <summary>
        /// 菜单
        /// </summary>
        public ITable<Menu> Menus { get { return this.GetTable<Menu>(); } }

        /// <summary>
        /// 权限
        /// </summary>
        public ITable<Permission> Permissions { get { return this.GetTable<Permission>(); } }

        /// <summary>
        /// 系统
        /// </summary>
        public ITable<SystemEntity> System { get { return this.GetTable<SystemEntity>();  } }

        /// <summary>
        /// 菜单权限关联
        /// </summary>
        public ITable<MenuPermissionAssociation> MenuPermissionAssociations { get { return this.GetTable<MenuPermissionAssociation>(); } }

        /// <summary>
        /// 角色权限关联
        /// </summary>
        public ITable<RolePermissionAssociation> RolePermissionAssociations { get { return this.GetTable<RolePermissionAssociation>(); } }

    }
}
