using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Grains;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Domain
{
    /// <summary>
    /// 菜单Grain接口
    /// </summary>
    [KingMetalObservable(name: nameof(IMenuGrain), topic: nameof(IMenuGrain), EventPartitions = 5, SnapshotPartitions = 5)]
    public interface IMenuGrain : IGrainWithIntegerKey, IKingMetalObservable
    {
    }
}
