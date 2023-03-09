using GoldCloud.Infrastructure.DataBase.Constants;
using LinqToDB.Mapping;

namespace GoldCloud.Infrastructure.DataBase.Entities
{
    /// <summary>
    /// 权限系统
    /// </summary>
    [Table(Schema = TableConsts.DbSchema, Name = TableConsts.SystemTable)]
    public class SystemEntity
    {
        #region 属性

        /// <summary>
        /// 系统标识
        /// </summary>
        [Column("id", IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("order")]
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }

        #endregion
    }
}
