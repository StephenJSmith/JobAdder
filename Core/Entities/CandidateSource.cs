using Newtonsoft.Json;

namespace Core.Entities
{
  public class CandidateSource
  {
    [JsonProperty("candidateId")]
    public long CandidateId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("skillTags")]
    public string SkillTags { get; set; }
  }
}