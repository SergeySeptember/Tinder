using Tinder.Models;
using Tinder.Models.Requests;

namespace Tinder.Interfaces
{
    public interface IActionMatches
    {
        IEnumerable<Matches> GetMatches();
        Matches CreateMatch(RequestMatchBody body);
        string DeleteMatch(int id);
    }
}
