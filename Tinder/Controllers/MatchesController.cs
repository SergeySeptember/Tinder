using Microsoft.AspNetCore.Mvc;
using Tinder.Models;
using Tinder.Services;

namespace Tinder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Matches> Get()
        {
            var matches = ActionMatches.GetMatches();
            return matches;
        }

        [HttpPost]
        public IActionResult Post(RequestMatchBody body)
        { 
            var createdMatch = ActionMatches.CreateMatch(body);

            if (createdMatch == null)
            {
                return BadRequest("Wrong id");
            }
            return Ok(createdMatch);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string answer = ActionMatches.DeleteMatch(id);
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
