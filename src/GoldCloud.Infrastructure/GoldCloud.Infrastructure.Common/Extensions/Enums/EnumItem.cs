namespace GoldCloud.Infrastructure.Common.Extensions.Enums
{
    /// <summary>
    ///
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 枚举显示值
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 枚举实际值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">枚举显示值</param>
        /// <param name="value">枚举实际值</param>
        public EnumItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}