using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class CandidatesController : BaseApiController
  {
    private readonly ICandidateService _candidateService;

    public CandidatesController(
      ICandidateService candidateService
      )
    {
      _candidateService = candidateService;
    }

    [HttpGet("source")]
    public async Task<ActionResult<IReadOnlyList<CandidateSource>>> GetSourceCandidates() {
      var candidates = await _candidateService.GetSourceCandidates();

      return Ok(candidates);
    }

    

    [HttpGet("weightings")]
    public ActionResult<IReadOnlyList<int>> GetSkillWeightings()
    {
        var weightings = _candidateService.GetStrengthWeightings();

        return Ok(weightings);
    }
  }
}