using System.Collections.Generic;

namespace Core.Entities
{
  public class Candidate
  {
    public int CandidateId { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SkillTags { get; set; }
    public IList<CandidateSkill> CandidateSkills { get; set; }
  }
}