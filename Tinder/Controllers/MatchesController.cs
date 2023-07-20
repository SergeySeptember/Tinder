using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tinder.Models;
using Tinder.Models.Requests;
using Tinder.Services;

namespace Tinder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly ActionMatches _actionMatches;
        public MatchesController(ActionMatches actionMatches)
        {
            _actionMatches = actionMatches;
        }
        [HttpGet]
        public IEnumerable<Matches> Get()
        {
            var matches = _actionMatches.GetMatches();
            return matches;
        }

        [HttpPost]
        public IActionResult Post(RequestMatchBody body)
        {
            var createdMatch = _actionMatches.CreateMatch(body);

            if (createdMatch == null)
            {
                return BadRequest("Wrong id");
            }
            return Ok(createdMatch);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string answer = _actionMatches.DeleteMatch(id);
            if (answer == "Match successfully deleted!")
            {
                return Ok(answer);
            }
            else
            {
                return BadRequest(answer);
            }
        }
    }
}
