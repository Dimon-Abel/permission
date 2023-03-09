using GoldCloud.Domain.Interfaces.Domain;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Handler;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Handler
{
    /// <summary>
    /// System DB Grain
    /// </summary>
    [KingMetalObserver(name: "system_db", group: "system_db", observable: typeof(ISystemGrain), Partitions = 5)]
    public interface ISystemDbGrain : IGrainWithIntegerKey, IKingMetalObserver
    {
    }

    /// <summary>
    /// System Flow Grain Interface
    /// </summary>
    [KingMetalObserver(name: "system_flow", group: "system_flow", observable: typeof(ISystemGrain), Partitions = 5)]
    public interface ISystemFlowGrain : IGrainWithIntegerKey, IKingMetalObserver
    {
    }
}
