using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface ICandidateService
    {
         IReadOnlyList<int> GetStrengthWeightings();
    }
}