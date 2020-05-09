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

    [HttpGet("source")]
    public async Task<ActionResult<IReadOnlyList<JobSource>>> GetSourceJobs() {
      var jobs = await _jobService.GetSourceJobs();

      return Ok(jobs);
    }

    [HttpGet("weightings")]
    public ActionResult<IReadOnlyList<int>> GetSkillWeightings()
    {
        var weightings = _jobService.GetRelevanceWeightings();

        return Ok(weightings);
    }
  }
}