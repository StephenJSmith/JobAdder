using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;



namespace Infrastructure.Services
{
  public class CandidateService : ICandidateService
  {
    private readonly IConfiguration _config;

    public CandidateService(IConfiguration config)
    {
      _config = config;
    }

    public IReadOnlyList<int> GetStrengthWeightings()
    {
        var key = "SkillsWeightings:CandidateStrengths";
        var value =_config[key];
        var weightings = value.Split(',').Select(int.Parse).ToList();

        return weightings;
    }
  }
}