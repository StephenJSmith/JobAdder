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
  public class JobService : IJobService
  {
    private readonly IConfiguration _config;
    private readonly ICandidateService _candidateService;

    public JobService(
      IConfiguration config,
      ICandidateService candidateService)
    {
      _config = config;
      _candidateService = candidateService;
    }

    public async Task<IReadOnlyList<JobSource>> GetSourceJobs()
    {
      var baseApiUrl = _config["BaseApiurl"];
      var baseAddress = new Uri(baseApiUrl);

      using (var httpClient = new HttpClient { BaseAddress = baseAddress })
      {
        try
        {
          using (var response = await httpClient.GetAsync("jobs"))
          {
            var responseData = await response.Content.ReadAsStringAsync();
            var jobs = JsonConvert.DeserializeObject<List<JobSource>>(responseData);

            return jobs;
          }
        }
        catch (System.Exception ex)
        {
          Console.WriteLine(ex);
          throw ex;
        }
      }
    }

    public async Task<JobSource> GetSourceJob(int jobId)
    {
      var sourceJobs = await GetSourceJobs();
      var sourceJob = sourceJobs.FirstOrDefault(j => j.JobId == jobId);

      return sourceJob;
    }

    public IReadOnlyList<int> GetRelevanceWeightings()
    {
      var key = "SkillsWeightings:JobRelevance";
      var value = _config[key];
      var weightings = value.Split(',').Select(int.Parse).ToList();

      return weightings;
    }

    public async Task<IReadOnlyList<Job>> GetJobsWithWeightedSkills()
    {
      var weightings = GetRelevanceWeightings();
      var sourceItems = await GetSourceJobs();

      var jobs = new List<Job>();
      foreach (var item in sourceItems)
      {
        var job = JobHelper.GetJobEntity(item, weightings);
        jobs.Add(job);
      }

      var orderedJobs = jobs
        .OrderBy(j => j.Company)
        .ThenBy(j => j.Name)
        .ToList();

      return orderedJobs;
    }

    public async Task<Job> GetJobWithWeightedSkills(int jobId)
    {
      var sourceJob = await GetSourceJob(jobId);
      if (sourceJob == null) { return null; }

      var jobWeightings = GetRelevanceWeightings();
      var weightedJob = JobHelper.GetJobEntity(sourceJob, jobWeightings);

      return weightedJob;
    }

    public async Task<IReadOnlyList<MatchedJobCandidate>> GetBestMatchedCandidatesForJob(int jobId, int number)
    {
      var job = await GetJobWithWeightedSkills(jobId);
      if (job == null) { return null; }

      var candidates = await _candidateService.GetCandidatesWithWeightedSkills();

      var matches = new List<MatchedJobCandidate>();
      foreach (var candidate in candidates)
      {
          var matchedSkills = JobHelper.GetJobCandidateMatchedSkills(job.JobSkills, candidate.CandidateSkills);
          if (!matchedSkills.Any()) { continue; }

          var matchedJobCandidate  = new MatchedJobCandidate {
            JobId = job.JobId,
            CandidateId = candidate.CandidateId,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            MatchedSkills = matchedSkills,
            SkillsCount = matchedSkills.Count,
            WeightingsSum = matchedSkills
              .Sum(ms => ms.JobWeighting * ms.CandidateWeighting)
          };

        matches.Add(matchedJobCandidate);
      }

      var orderedMatches = matches
        .OrderByDescending(m => m.WeightingsSum)
        .ThenByDescending(m => m.SkillsCount)
        .Take(number)
        .ToList();

      return orderedMatches;
    }
  }
}