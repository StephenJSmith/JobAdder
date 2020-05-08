using System.Collections.Generic;

namespace Core.Entities
{
    public class Job
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public IList<JobSkill> JobSkills { get; set; }
    }
}