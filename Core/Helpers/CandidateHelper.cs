using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Helpers
{
  public class CandidateHelper
  {
    public static IList<CandidateSkill> GetCandidateSkills(
        int candidateId,
        string skillTags,
        IReadOnlyList<int> weightings)
    {
      var skillTagsList = skillTags
        .Replace(" ", "")
        .Split(',')
        .Distinct()
        .ToList();

      var candidateSkills = new List<CandidateSkill>();
      var skillsCount = skillTagsList.Count;
      for (int i = 0; i < skillsCount; i++)
      {
        var name = skillTagsList[i];
        var weighting = 0;
        if (i < weightings.Count)
        {
          weighting = weightings[i];
        }

        candidateSkills.Add(new CandidateSkill
        {
          CandidateId = candidateId,
          Name = name,
          Weighting = weighting
        });
      }

      return candidateSkills;
    }

    public static string GetCandidatesSkillsCsv(IList<CandidateSkill> skills) {
      var toArray = skills.Select(s => s.Name).ToArray();
      var csv = string.Join(", ", toArray);

      return csv;
    }
  }
}