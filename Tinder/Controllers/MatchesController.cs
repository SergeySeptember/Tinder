using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tinder.Interfaces;
using Tinder.Models;
using Tinder.Models.Requests;

namespace Tinder.Controllers
{
    [Route("matches")]
    [ApiController]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly IActionMatches _actionMatches;
        public MatchesController(IActionMatches actionMatches)
        {
            _actionMatches = actionMatches;
        }

        /// <summary>
        /// Get the list of matches
        /// </summary>
        /// <returns>List of matches</returns>
        /// <response code="200">Returns the list of matches</response>
        [ProducesResponseType(typeof(IEnumerable<Matches>), 200)]
        [Produces("application/json")]
        [HttpGet]
        public IActionResult Get()
        {
            var matches = _actionMatches.GetMatches();
            return Ok(matches);
        }

        /// <summary>
        /// Create a new match
        /// </summary>
        /// <param name="body">Match data to create.</param>
        /// <returns>Created new match with ID and time craete</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /matches
        ///     {
        ///        "firstUserId": 0,
        ///        "secondUserId": 0
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the created match with ID and time craete</response>
        /// <response code="400">Some data is invalid</response>
        [ProducesResponseType(typeof(Users), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [Produces("application/json")]
        [HttpPost]
        public IActionResult Post(RequestMatchBody body)
        {
            var createdMatch = _actionMatches.CreateMatch(body);

            if (createdMatch == null)
            {
                return BadRequest("Wrong id");
            }
            return Created($"Empty", createdMatch);
        }

        /// <summary>
        /// Delete a match by ID
        /// </summary>
        /// <param name="id">Match ID</param>
        /// <returns>Match deletion status response</returns>
        /// <response code="200">Match successfully deleted!</response>
        /// <response code="400">Match not found</response>
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
