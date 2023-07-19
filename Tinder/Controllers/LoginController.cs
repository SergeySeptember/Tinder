using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tinder.Authentication;


[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    public IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpPost]
    public IActionResult Login(User user)
    {
        if (user != null && user.UserName != null && user.Password != null)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt> ();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", "1"),
                new Claim("UserName", "admin"),
                new Claim("Password", "admin")
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
            return BadRequest("Error");
        }
    }
}