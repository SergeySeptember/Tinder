using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class UsersList
    {
        [JsonPropertyName("users")]
        public List<Users> Users { get; set; }
    }
}
