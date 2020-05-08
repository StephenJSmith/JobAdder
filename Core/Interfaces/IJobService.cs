using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IJobService
    {
        IReadOnlyList<int> GetRelevanceWeightings();
     }
}