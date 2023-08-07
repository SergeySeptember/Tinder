using System.Text.Json.Serialization;

namespace Tinder.Models.Requests
{
    public class RequestUserBody
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
