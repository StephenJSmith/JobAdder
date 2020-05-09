using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface ICandidateService
    {
         IReadOnlyList<int> GetStrengthWeightings();
         Task<IReadOnlyList<CandidateSource>> GetSourceCandidates();
         Task<IReadOnlyList<Candidate>> GetPopulatedEntities();
    }
}