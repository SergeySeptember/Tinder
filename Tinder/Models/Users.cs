using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
    }
}
