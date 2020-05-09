using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
  public class CandidateService : ICandidateService
  {
    private readonly IConfiguration _config;

    public CandidateService(IConfiguration config)
    {
      _config = config;
    }

    public async Task<IReadOnlyList<CandidateSource>> GetSourceCandidates()
    {
      var baseApiUrl = _config["BaseApiurl"];
      var baseAddress = new Uri(baseApiUrl);

      using (var httpClient = new HttpClient { BaseAddress = baseAddress })
      {
        try
        {
          using (var response = await httpClient.GetAsync("candidates"))
          {
            var responseData = await response.Content.ReadAsStringAsync();
            var candidates = JsonConvert.DeserializeObject<List<CandidateSource>>(responseData);

            return candidates;
          }
        }
        catch (System.Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }
      }
    }

    public IReadOnlyList<int> GetStrengthWeightings()
    {
      var key = "SkillsWeightings:CandidateStrengths";
      var value = _config[key];
      var weightings = value.Split(',').Select(int.Parse).ToList();

      return weightings;
    }

    public async Task<IReadOnlyList<Candidate>> GetPopulatedEntities()
    {
      var weightings = GetStrengthWeightings();
      var sourceItems = await GetSourceCandidates();
      var candidateHelper = new CandidateHelper();

      var candidates = new List<Candidate>();
      foreach (var item in sourceItems)
      {
        var candidate = GetCandidateEntity(item, weightings, candidateHelper);
        candidates.Add(candidate);
      }

      return candidates;
    }

    private Candidate GetCandidateEntity(
      CandidateSource item,
      IReadOnlyList<int> weightings,
      CandidateHelper helper)
    {
      var candidate = new Candidate
      {
        CandidateId = item.CandidateId,
        FirstName = item.Name.Split(' ').First(),
        LastName = item.Name.Split(' ').Last(),
        SkillTags = item.SkillTags,
        CandidateSkills = helper.GetCandidateSkills(
          item.CandidateId, item.SkillTags, weightings)
      };

      return candidate;
    }
  }
}