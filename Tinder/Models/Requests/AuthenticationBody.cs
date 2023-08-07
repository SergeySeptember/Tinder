using System.Text.Json.Serialization;

namespace Tinder.Models.Requests
{
    public class AuthenticationBody
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
