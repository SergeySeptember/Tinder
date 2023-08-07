using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tinder.Models;
using Tinder.Models.Requests;
using Tinder.Services.AuthenticationAndAuthorization;

[Route("login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Authorization _authorization;
    public LoginController(IConfiguration configuration, Authorization authorization)
    {
        _configuration = configuration;
        _authorization = authorization;
    }

    /// <summary>
    /// Get JWT token for authorization
    /// </summary>
    /// <returns>JWT Token for authorization</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Post /login
    ///     {
    ///        "userName": "Name",
    ///        "password": "Example2)"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the token for authorization</response>
    /// <response code="400">Invalid login or password</response>
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [Produces("text/plain")]
    [HttpPost]
    public IActionResult Login([FromBody] AuthenticationBody user)
    {
        string answer = _authorization.Authorize(user);
        if (answer == "Invalid login or password")
        {
            return BadRequest(answer);
        }
        else
        {
            return Ok(answer);
        }
    }
}