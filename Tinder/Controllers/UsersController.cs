using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tinder.Interfaces;
using Tinder.Models;
using Tinder.Models.Requests;
using Tinder.Services;

namespace Tinder.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IActionUsers _actionUsers;

        public UsersController(IActionUsers actionUsers)
        {
            _actionUsers = actionUsers;
        }

        /// <summary>
        /// Get the list of users
        /// </summary>
        /// <returns>List of users</returns>
        /// <response code="200">Returns the list of users</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Users>), 200)]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var users = _actionUsers.GetUsers();
            var response = new UsersList { Users = users };
            return Ok(response);
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="User ID">User ID</param>
        /// <returns>User by ID</returns>
        /// <response code="200">Returns the user by ID</response>
        /// <response code="404">User not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            Users user = _actionUsers.GetUserById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="body">User data to create.</param>
        /// <returns>Created new user with ID and without password</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /users
        ///     {
        ///        "userName": "Name",
        ///        "email": "example@mail.ru",
        ///        "password": "Example2)",
        ///        "age":25,
        ///        "location": "City"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the created user with ID and without the password</response>
        /// <response code="400">Some data is invalid</response>
        [ProducesResponseType(typeof(Users), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [Produces("application/json", "text/plain")]
        [HttpPost]
        public IActionResult Post(RequestUserBody body)
        {
            string valid = Validation.ValidationData(body);
            if (valid == "true")
            {
                var createdUser = _actionUsers.CreatetUser(body);
                return Created($"https://localhost:7227/users/{createdUser.Id}", createdUser);
            }
            else
            {
                return BadRequest(valid);
            }
        }

        /// <summary>
        /// Update user data by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="body">User data to update.</param>
        /// <returns>Changed user data with id and without password</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /users/{id}
        ///     {
        ///        "userName": "Name",
        ///        "email": "example@mail.ru",
        ///        "password": "Example2)",
        ///        "age":25,
        ///        "location": "City"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the updated user data with ID and without the password</response>
        /// <response code="400">Some data is invalid</response>
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [Produces("application/json")]
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

        /// <summary>
        /// Delete a user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User deletion status response</returns>
        /// <response code="200">User successfully deleted!</response>
        /// <response code="400">User not found</response>
        [ProducesResponseType(typeof(Users), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [Produces("application/json")]
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
