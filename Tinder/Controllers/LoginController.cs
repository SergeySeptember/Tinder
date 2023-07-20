using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tinder.Models;
using Tinder.Models.Requests;
using Tinder.Services;


[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    public IConfiguration _configuration;
    private readonly Authentication _authentication;

    public LoginController(IConfiguration configuration, Authentication authentication)
    {
        _configuration = configuration;
        _authentication = authentication;
    }

    [HttpPost]
    public IActionResult Login(AuthenticationBody user)
    {
        if (_authentication.AuthenticationUser(user))
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Password", user.Password)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signIn
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        else
        {
            return BadRequest("Invalid login or password");
        }
    }
}