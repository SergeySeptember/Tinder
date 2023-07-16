namespace Tinder.Models
{
    public class Matches
    {
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public string MatchDate { get; set; }

        public Matches()
        {
            MatchDate = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
}
