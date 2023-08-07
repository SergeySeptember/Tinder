using Tinder.Interfaces;
using Tinder.Models;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public class ActionMatches : IActionMatches
    {
        private readonly Context _context;

        public ActionMatches(Context context)
        {
            _context = context;
        }
        public IEnumerable<Matches> GetMatches()
        {
            var allMatches = _context.Matches.ToList();
            return allMatches;
        }

        public Matches CreateMatch(RequestMatchBody body)
        {
            var checkMatch = _context.Matches.FirstOrDefault(u => u.FirstUserId == body.FirstUserId && u.SecondUserId == body.SecondUserId);

            if (checkMatch == null && body.FirstUserId != body.SecondUserId)
            {
                _context.Matches.AddRange(new Matches { FirstUserId = body.FirstUserId, SecondUserId = body.SecondUserId });
                _context.SaveChanges();

                var createdMatch = _context.Matches.FirstOrDefault(u => u.FirstUserId == body.FirstUserId && u.SecondUserId == body.SecondUserId);
                return createdMatch;
            }
            else
            {
                return null;
            }
        }

        public string DeleteMatch(int id)
        {
            var item = _context.Matches.Find(id);
            if (item != null)
            {
                _context.Matches.Remove(item);
                _context.SaveChanges();
                return "Match successfully deleted!";
            }
            else
            {
                return "Match not found";
            }
        }
    }
}
