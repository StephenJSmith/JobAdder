using Newtonsoft.Json;

namespace Core.Entities
{
  public class JobSource
  {
    [JsonProperty("jobId")]
    public int JobId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("company")]
    public string Company { get; set; }

    [JsonProperty("skills")]
    public string Skills { get; set; }
  }
}