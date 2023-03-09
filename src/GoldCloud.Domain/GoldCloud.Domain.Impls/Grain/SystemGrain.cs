using System.Threading.Tasks;
using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Commands.System;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Domain.Interfaces.Events.System;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Shared.Exception;
using KingMetal.Domains.Core.Grains;

namespace GoldCloud.Domain.Impls.Grain
{
    /// <summary>
    /// 系统 Grain
    /// </summary>
    public class SystemGrain : DomainGrain<SystemState, long>, ISystemGrain
    {
        #region 增删改

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="command"><see cref="CreateSystemCommand" /></param>
        /// <returns></returns>
        public async Task Handler(CreateSystemCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new GoldCloudException(ErrorCode.InvalidArgument, "系统名称不能为空");

            await RaiseEvent(command.CommandId, new SystemCreateEvent()
            {
                Name = command.Name,
                Order = command.Order,
                Remark = command.Remark
            });
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="command"><see cref="UpdatePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(UpdateSystemCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new GoldCloudException(ErrorCode.InvalidArgument, "系统名称不能为空");

            await RaiseEvent(command.CommandId, new SystemUpdateEvent()
            {
                Name = command.Name,
                Order = command.Order,
                Remark = command.Remark
            });
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="command"><see cref="DeletePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(DeleteSystemCommand command) =>
            await RaiseEvent(command.CommandId, new SystemDeleteEvent());


        #endregion
    }
}
