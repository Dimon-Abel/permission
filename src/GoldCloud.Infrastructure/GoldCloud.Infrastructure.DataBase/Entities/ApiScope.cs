using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.ApiScope)]
    public class ApiScope
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Column("id"), NotNull]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("name"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        [Column("required"), NotNull]
        public bool Required { get; set; }

        /// <summary>
        /// 是否强调
        /// </summary>
        [Column("emphasize"), NotNull]
        public bool Emphasize { get; set; }

        /// <summary>
        /// 是否显示在发掘文档
        /// </summary>
        [Column("show_in_discovery_document"), NotNull]
        public bool ShowInDiscoveryDocument { get; set; }

        /// <summary>
        /// Api资源标识
        /// </summary>
        [Column("api_resource_id")]
        public long ApiResourceId { get; set; }

        /// <summary>
        /// 关联关系
        /// </summary>
        [Association(ThisKey = nameof(ApiResourceId), OtherKey = nameof(ApiResource.Id))]
        public ApiResource ApiResourceEntity { get; set; }
    }
}
