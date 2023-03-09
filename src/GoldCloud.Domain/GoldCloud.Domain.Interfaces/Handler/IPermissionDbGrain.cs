using GoldCloud.Domain.Interfaces.Domain;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Handler;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Handler
{
    /// <summary>
    /// Permission DB Grain Interface
    /// </summary>
    [KingMetalObserver(name: "permission_db", group: "permission_db", observable: typeof(IPermissionGrain), Partitions = 5)]
    public interface IPermissionDbGrain: IGrainWithIntegerKey, IKingMetalObserver
    {
    }

    /// <summary>
    /// Permission DB Grain Interface
    /// </summary>
    [KingMetalObserver(name: "permission_flow", group: "permission_flow", observable: typeof(IPermissionGrain), Partitions = 5)]
    public interface IPermissionFlowGrain: IGrainWithIntegerKey, IKingMetalObserver
    {
    }
}
