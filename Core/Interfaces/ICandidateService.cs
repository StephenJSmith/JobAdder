using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Core.Specifications;

namespace Core.Interfaces
{
  public interface ICandidateService
    {
         IReadOnlyList<int> GetStrengthWeightings();
         Task<IReadOnlyList<CandidateSource>> GetSourceCandidates();
         Task<IReadOnlyList<Candidate>> GetCandidatesWithWeightedSkills();
         Task<Pagination<Candidate>> GetPagedCandidatesWithWeightedSkills(PageSpecParams pageParams);
    }
}