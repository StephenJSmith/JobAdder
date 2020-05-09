using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
  public interface IJobService
  {
    IReadOnlyList<int> GetRelevanceWeightings();

    Task<IReadOnlyList<JobSource>> GetSourceJobs();

    Task<JobSource> GetSourceJob(int jobId);

    Task<IReadOnlyList<Job>> GetJobsWithWeightedSkills();

    Task<IReadOnlyList<Job>> GetBestMatchedCandidatesForJob(int jobId, int number);
  }
}