using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
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

    public async Task<IReadOnlyList<Candidate>> GetCandidatesWithWeightedSkills()
    {
      var orderedCandidates = await RetrieveAllCandidatesWithWeightedSkills();

      return orderedCandidates;
    }

    private async Task<IReadOnlyList<Candidate>> RetrieveAllCandidatesWithWeightedSkills()
    {
      var weightings = GetStrengthWeightings();
      var sourceItems = await GetSourceCandidates();

      var candidates = new List<Candidate>();
      foreach (var item in sourceItems)
      {
        var candidate = CandidateHelper.GetCandidateEntity(item, weightings);
        candidates.Add(candidate);
      }

      var orderedCandidates = candidates
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .ToList();

      return orderedCandidates;
    }

    public async Task<Pagination<Candidate>> GetPagedCandidatesWithWeightedSkills(PageSpecParams pageParams)
    {
      var allCandidates = await RetrieveAllCandidatesWithWeightedSkills();
      var count = allCandidates.Count;
      var pagedCandidates = allCandidates
        .Skip(pageParams.Skip)
        .Take(pageParams.Take)
        .ToList();
      var pagination = new Pagination<Candidate>(
        pageParams.PageNumber, pageParams.PageSize, count, pagedCandidates);

      return pagination;
    }
  }
}