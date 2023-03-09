using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Grains;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Domain
{
    /// <summary>
    /// 权限Grain接口
    /// </summary>
    [KingMetalObservable(name: nameof(IPermissionGrain), topic: nameof(IPermissionGrain), EventPartitions = 5, SnapshotPartitions = 5)]
    public interface IPermissionGrain : IGrainWithIntegerKey, IKingMetalObservable
    {
    }
}
