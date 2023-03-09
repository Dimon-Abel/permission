using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 更新权限资源
    /// </summary>
    [Command(Name = nameof(UpdateResourceCommand))]
    public class UpdateResourceCommand : KingMetalCommand
    {
        /// <summary>
        /// 资源
        /// </summary>
        public List<ApiScopeEntity> Resource { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public UpdateResourceCommand()
        {
            Resource = new List<ApiScopeEntity>();
        }
    }
}
