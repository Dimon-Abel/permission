using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Grains;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Domain
{
    /// <summary>
    /// 系统Grain接口
    /// </summary>
    [KingMetalObservable(name: nameof(ISystemGrain), topic: nameof(ISystemGrain), EventPartitions = 5, SnapshotPartitions = 5)]
    public interface ISystemGrain : IGrainWithIntegerKey, IKingMetalObservable
    {
    }
}
