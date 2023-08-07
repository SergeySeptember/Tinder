using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Matches
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstUserId")]
        public int FirstUserId { get; set; }
        [JsonPropertyName("secondUserId")]
        public int SecondUserId { get; set; }
        [JsonPropertyName("matchDate")]
        public string MatchDate { get; set; }

        public Matches()
        {
            MatchDate = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
}
