using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Users
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
