namespace Patient_Health_Management_System.Domain.Forms
{
    public class AccountForm
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
		[JsonPropertyName("password")]
		public string Password { get; set; }
		[JsonPropertyName("connection")]
		public string Connection { get; set;}
    }
}