namespace Patient_Health_Management_System.Models
{
    public class Auth0Token
    {
        public string access_token { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}