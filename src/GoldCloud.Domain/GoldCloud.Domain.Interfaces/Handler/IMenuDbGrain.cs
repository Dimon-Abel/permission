using GoldCloud.Domain.Interfaces.Domain;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Handler;
using Orleans;

namespace GoldCloud.Domain.Interfaces.Handler
{
    /// <summary>
    /// Menu DB Grain Interface
    /// </summary>
    [KingMetalObserver(name: "menu_db", group: "menu_db", observable: typeof(IMenuGrain), Partitions = 5)]
    public interface IMenuDbGrain : IGrainWithIntegerKey, IKingMetalObserver
    {
    }

    /// <summary>
    /// Menu Flow Grain Interface
    /// </summary>
    [KingMetalObserver(name: "menu_flow", group: "menu_flow", observable: typeof(IMenuGrain), Partitions = 5)]
    public interface IMenuFlowGrain : IGrainWithIntegerKey, IKingMetalObserver
    {
    }
}
