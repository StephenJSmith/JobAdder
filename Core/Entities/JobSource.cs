namespace Core.Entities
{
  public class JobSource
  {
    // [JsonProperty("jobId")]
    public long JobId { get; set; }

    // [JsonProperty("name")]
    public string Name { get; set; }

    // [JsonProperty("company")]
    public string Company { get; set; }

    // [JsonProperty("skills")]
    public string Skills { get; set; }
  }
}