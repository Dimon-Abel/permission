using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.System
{
    /// <summary>
    /// 更新系统
    /// </summary>
    [Command(Name = nameof(UpdateSystemCommand))]
    public class UpdateSystemCommand : KingMetalCommand
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
