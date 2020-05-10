using System.Collections.Generic;

namespace Core.Entities
{
    public class MatchedJobCandidate
    {
        public int JobId { get; set; }
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WeightingsSum { get; set; }
        public int SkillsCount { get; set; }
        public IList<MatchedSkill> MatchedSkills { get; set; }
    }
}