using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.State;

namespace GoldCloud.Domain.Impls.State
{
    /// <summary>
    /// 系统 State
    /// </summary>
    public class SystemState : IState<SystemState>
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

        public SystemState Clone() => new SystemState();
    }
}
