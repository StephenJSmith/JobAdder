namespace Core.Entities
{
  public class MatchedSkill
  {
    public int JobId { get; set; }
    public int CandidateId { get; set; }
    public string Name { get; set; }
    public int JobWeighting { get; set; }
    public int CandidateWeighting { get; set; }
  }
}