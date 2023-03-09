namespace GoldCloud.Infrastructure.Shared.ValueObjects
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }
    }
}
