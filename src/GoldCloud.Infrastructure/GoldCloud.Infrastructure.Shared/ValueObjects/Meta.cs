namespace GoldCloud.Infrastructure.Shared.ValueObjects
{
    /// <summary>
    /// route meta
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// svg图标
        /// </summary>
        public string ElSvgIcon { get; set; }

        /// <summary>
        /// 是否缓存界面
        /// </summary>
        public bool CachePage { get; set; } = false;

        /// <summary>
        /// 关闭tab是否清理缓存
        /// </summary>
        public bool CloseTabRmCache { get; set; } = false;

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
    }
}
