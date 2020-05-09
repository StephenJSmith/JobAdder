using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Helpers
{
  public class CandidateHelper
  {
    public static Candidate GetCandidateEntity(
      CandidateSource item,
      IReadOnlyList<int> weightings)
    {
      var candidate = new Candidate
      {
        CandidateId = item.CandidateId,
        Name = item.Name,
        FirstName = CandidateHelper.GetFirstName(item.Name),
        LastName = CandidateHelper.GetLastName(item.Name),
        CandidateSkills = CandidateHelper.GetCandidateSkills(
          item.CandidateId, item.SkillTags, weightings)
      };

      candidate.SkillTags = CandidateHelper.GetCandidatesSkillsCsv(candidate.CandidateSkills);

      return candidate;
    }

    public static string GetFirstName(string name = "") {
      if (name == null) { return string.Empty; }
      
      var names = name.Split(' ');
      if (names.Length < 2) { return string.Empty; }

      return names.First();
    }

    public static string GetLastName(string name = "") {
      if (name == null) { return string.Empty; }

      var names = name.Split(' ');
      if (names.Length == 0) { return string.Empty; }

      return names.Last();
    }

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