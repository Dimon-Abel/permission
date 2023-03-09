using Orleans;
using System.Threading.Tasks;

namespace GoldCloud.Domain.Interfaces.Common
{
    public static class GrainExtension
    {
        public static async Task<long> NewInterIdAsync(this IGrainFactory grainFactory)
        {
            return await grainFactory.GetUniqueIdService().NewIntegerId();
        }
    }
}
