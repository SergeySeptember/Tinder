using Tinder.Models;

namespace Tinder.Services
{
    public class ActionMatches
    {
        public static IEnumerable<Matches> GetMatches()
        {
            using (Context db = new())
            {
                var allMatches = db.Matches.ToList();
                return allMatches;
            };
        }

        public static Matches CreateMatch(RequestMatchBody body)
        {
            using (Context db = new())
            {
                var checkMatch = db.Matches.FirstOrDefault(u => u.FirstUserId == body.FirstUserId && u.SecondUserId == body.SecondUserId);

                if (checkMatch == null && body.FirstUserId != body.SecondUserId)
                {
                    db.Matches.AddRange(new Matches { FirstUserId = body.FirstUserId, SecondUserId = body.SecondUserId });
                    db.SaveChanges();

                    var createdMatch = db.Matches.FirstOrDefault(u => u.FirstUserId == body.FirstUserId && u.SecondUserId == body.SecondUserId);
                    return createdMatch;
                }
                else
                {
                    return null;
                }
            };
        }

        public static string DeleteMatch(int id)
        {
            using (Context db = new())
            {
                var item = db.Matches.Find(id);
                if (item != null)
                {
                    db.Matches.Remove(item);
                    db.SaveChanges();
                    return "Match successfully deleted!";
                }
                else
                {
                    return "Match not found";
                }
            };
        }
    }
}
