namespace Patient_Health_Management_System.Domain.DTOs
{
    public class UserResponse
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("picture")]
        public string Picture { get; set; }
    }
}
