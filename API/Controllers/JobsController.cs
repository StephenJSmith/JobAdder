using System.Collections.Generic;
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

    [HttpGet("weightings")]
    public ActionResult<IReadOnlyList<int>> GetSkillWeightings()
    {
        var weightings = _jobService.GetRelevanceWeightings();

        return Ok(weightings);
    }
  }
}