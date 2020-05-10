using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class JobsController : BaseApiController
  {
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
      _jobService = jobService;
    }

    // TODO: Use of DTOs to return shaped data in responses rather than Entities
    // TODO: AutoMapper to map between Entities and DTOs

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Job>>> GetJobsWithWeightedSkills() {
      var jobs = await _jobService.GetJobsWithWeightedSkills();

      return Ok(jobs);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<Job>> GetJobWithWeightedSkills(int jobId) {
      var job = await _jobService.GetJobWithWeightedSkills(jobId);
      if (job == null) { return NotFound(); }

      return Ok(job);
    }

    [HttpGet("{jobId}/candidates/{number}")]
    public async Task<ActionResult<IReadOnlyList<MatchedJobCandidate>>> GetBestMatchedCandidatesForJob(int jobId, int number) {
      var matchedCandidates = await _jobService.GetBestMatchedCandidatesForJob(jobId, number);

      return Ok(matchedCandidates);
    }

    [HttpGet("source")]
    public async Task<ActionResult<IReadOnlyList<JobSource>>> GetSourceJobs() {
      var jobs = await _jobService.GetSourceJobs();

      return Ok(jobs);
    }

    [HttpGet("source/{jobId}")]
    public async Task<ActionResult<JobSource>> GetSourceJob(int jobId) {
      var job = await _jobService.GetSourceJob(jobId);
      if (job == null) { return NotFound(); }

      return Ok(job);
    }

    [HttpGet("weightings")]
    public ActionResult<IReadOnlyList<int>> GetSkillWeightings()
    {
        var weightings = _jobService.GetRelevanceWeightings();

        return Ok(weightings);
    }
  }
}