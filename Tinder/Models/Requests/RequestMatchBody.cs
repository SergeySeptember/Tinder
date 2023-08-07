using System.Text.Json.Serialization;

namespace Tinder.Models.Requests
{
    public class RequestMatchBody
    {
        [JsonPropertyName("firstUserId")]
        public int FirstUserId { get; set; }
        [JsonPropertyName("secondUserId")]
        public int SecondUserId { get; set; }
    }
}
