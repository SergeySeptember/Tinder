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
    public class UsersController : ControllerBase
    {
        private readonly ActionUsers _actionUsers;
        public UsersController(ActionUsers actionUsers)
        {
            _actionUsers = actionUsers;
        }
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            var users = _actionUsers.GetUsers();
            return users;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Users user = _actionUsers.GetUserById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpPost]
        public IActionResult Post(RequestUserBody body)
        {
            string valid = Validation.ValidationData(body);
            if (valid == "true")
            {
                var createdUser = _actionUsers.CreatetUser(body);
                return Ok(createdUser);
            }
            else
            {
                return BadRequest(valid);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RequestUserBody body)
        {
            string valid = Validation.ValidationData(body);
            if (valid == "true")
            {
                var createdUser = _actionUsers.UpdateUser(id, body);
                return Ok(createdUser);
            }
            else
            {
                return BadRequest(valid);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string answer = _actionUsers.DeleteUser(id);
            if (answer == "User successfully deleted!")
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
