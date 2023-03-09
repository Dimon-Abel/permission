using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.System
{
    /// <summary>
    /// 创建系统
    /// </summary>
    [Command(Name = nameof(CreateSystemCommand))]
    public class CreateSystemCommand : KingMetalCommand
    {
        #region 属性

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
}
