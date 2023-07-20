namespace Tinder.Models.Requests
{
    public class RequestUserBody
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
    }
}
