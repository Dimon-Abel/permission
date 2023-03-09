using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB.Mapping;
using System.Collections.Generic;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.ApiResource)]
    public class ApiResource
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Column("id"), NotNull]
        public long Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("enabled"), NotNull]
        public bool Enabled { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        [Column("name"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 资源说明
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created"), NotNull]
        public long Created { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("updated")]
        public long Updated { get; set; }

        /// <summary>
        /// 最后一次访问时间
        /// </summary>
        [Column("last_accessed")]
        public long LastAccessed { get; set; }

        /// <summary>
        /// 是否不可编辑
        /// </summary>
        [Column("non_editable"), NotNull]
        public bool NonEditable { get; set; }

        /// <summary>
        /// 关联关系
        /// </summary>
        [Association(ThisKey = nameof(Id), OtherKey = nameof(ApiScope.ApiResourceId))]
        public List<ApiScope> ApiScopeEntites { get; set; } = new List<ApiScope>();
    }
}
