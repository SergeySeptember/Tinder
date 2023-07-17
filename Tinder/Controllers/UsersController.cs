using Microsoft.AspNetCore.Mvc;
using Tinder.Models;
using Tinder.Services;

namespace Tinder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            var users = ActionUsers.GetUsers();
            return users;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Users user = ActionUsers.GetUserById(id);

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
            string valid = ActionUsers.Validation(body);
            if (valid == "true")
            {
                var createdUser = ActionUsers.CreatetUser(body);
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
            string valid = ActionUsers.Validation(body);
            if (valid == "true")
            {
                var createdUser = ActionUsers.UpdateUser(id, body);
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
            string answer = ActionUsers.DeleteUser(id);
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
