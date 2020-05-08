using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
  public class JobService : IJobService
  {
    private readonly IConfiguration _config;
    
    public JobService(IConfiguration config)
    {
      _config = config;
    }

    public IReadOnlyList<int> GetRelevanceWeightings()
    {
      var key = "SkillsWeightings:JobRelevance";
      var value = _config[key];
      var weightings = value.Split(',').Select(int.Parse).ToList();

      return weightings;
    }
  }
}