using System.Collections.Generic;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class CandidatesController : BaseApiController
  {
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
      _candidateService = candidateService;
    }

    [HttpGet("weightings")]
    public ActionResult<IReadOnlyList<int>> GetSkillWeightings()
    {
        var weightings = _candidateService.GetStrengthWeightings();

        return Ok(weightings);
    }
  }
}