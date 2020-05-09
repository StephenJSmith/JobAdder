using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Helpers
{
  public class JobHelper
  {
  public static Job GetJobEntity(JobSource item, IReadOnlyList<int> weightings) {
      var job = new Job {
        JobId = item.JobId,
        Name = item.Name,
        Company = item.Company,
        Skills = item.Skills,
        JobSkills = JobHelper.GetJobSkills(
          item.JobId, item.Skills, weightings)
      };

      job.Skills = JobHelper.GetJobSkillsCsv(job.JobSkills);

      return job;
    }

    public static IList<JobSkill> GetJobSkills(
        int jobId,
        string skills,
        IReadOnlyList<int> weightings)
    {
      var skillsList = skills
          .Replace(" ", "")
          .Split(',')
          .Distinct()
          .ToList();

      var jobSkills = new List<JobSkill>();
      var skillsCount = skillsList.Count;
      for (int i = 0; i < skillsCount; i++)
      {
        var name = skillsList[i];
        var weighting = 0;
        if (i < weightings.Count)
        {
          weighting = weightings[i];
        }

        jobSkills.Add(new JobSkill
        {
          JobId = jobId,
          Name = name,
          Weighting = weighting
        });
      }

      return jobSkills;
    }

    public static string GetJobSkillsCsv(IList<JobSkill> skills)
    {
      var toArray = skills.Select(s => s.Name).ToArray();
      var csv = string.Join(", ", toArray);

      return csv;
    }
  }
}