namespace Hust_Medical.Domain.Models
{
    public class Role
    {
        [JsonPropertyName("id")]
        public string Id { get; set;}
        [JsonPropertyName("name")]
        public string Name { get; set;}
        [JsonPropertyName("description")]
        public string Description { get; set;}
    }
}