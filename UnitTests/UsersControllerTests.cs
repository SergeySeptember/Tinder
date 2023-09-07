using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Tinder.Controllers;
using Tinder.Interfaces;
using Tinder.Models;
using Tinder.Models.Requests;
using Tinder.Services;

namespace UnitTests
{
    public class UsersControllerTests
    {
        [Fact]
        public void Get_ReturnsUsers()
        {
            // Arrange
            var mockActionUsers = new Mock<IActionUsers>();
            var expectedUsers = new List<Users>
            {
                new Users { Id = 1, UserName = "user1" },
                new Users { Id = 2, UserName = "user2" }
            };
            mockActionUsers.Setup(x => x.GetUsers()).Returns(expectedUsers);

            var controller = new UsersController(mockActionUsers.Object);

            // Act
            var result = controller.Get() as OkObjectResult;
            var data = (UsersList)result.Value;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(expectedUsers.Count, data.Users.Count);
        }

        [Fact]
        public void Get_ReturnsUserById_WhenUserExists()
        {
            // Arrange
            var mockActionUsers = new Mock<IActionUsers>();
            var expectedUser = new Users { Id = 1, UserName = "user1" };
            mockActionUsers.Setup(x => x.GetUserById(1)).Returns(expectedUser);

            var controller = new UsersController(mockActionUsers.Object);

            // Act
            var result = controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUser = Assert.IsAssignableFrom<Users>(okResult.Value);

            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
        }

        [Fact]
        public void Get_ReturnsNotFoundById_WhenUserDoesNotExist()
        {
            // Arrange
            var mockActionUsers = new Mock<IActionUsers>();
            Users expectedUser = null;
            mockActionUsers.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(expectedUser);

            var controller = new UsersController(mockActionUsers.Object);

            // Act
            var result = controller.Get(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public void PostUser_CreatesUserAndReturnIt()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(databaseName: "test_db").Options;
            
            var passwordHashingService = new PasswordHashing();

            using (var context = new Context(options))
            {
                var actionUsers = new ActionUsers(context, passwordHashingService);

                var requestUserBody = new RequestUserBody
                {
                    UserName = "Alex",
                    Email = "test@example.com",
                    Age = 30,
                    Location = "Test City",
                    Password = "Galaxytab2)"
                };

                // Act
                var createdUser = actionUsers.CreatetUser(requestUserBody);

                // Assert
                Assert.NotNull(createdUser);
                Assert.Equal("Alex", createdUser.UserName);
                Assert.Equal("test@example.com", createdUser.Email);
                Assert.Equal(30, createdUser.Age);
                Assert.Equal("Test City", createdUser.Location);
                Assert.Equal(passwordHashingService.HashPassword("Galaxytab2)"), createdUser.Password);
            }

            using (var context = new Context(options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
        public void PutUsers_UpdateUser_ReturnsOkResultWithUpdatedUser()
        {
            // Arrange
            int userId = 1;
            var mockActionUsers = new Mock<IActionUsers>();
            var expectedUpdatedUser = new Users
            {
                Id = userId,
                UserName = "UpdatedUser",
                Email = "updated@example.com",
                Age = 30,
                Location = "UpdatedLocation"
            };
            mockActionUsers.Setup(x => x.UpdateUser(userId, It.IsAny<RequestUserBody>())).Returns(expectedUpdatedUser);

            var controller = new UsersController(mockActionUsers.Object);

            var body = new RequestUserBody
            {
                UserName = "UpdatedUser",
                Email = "updated@example.com",
                Age = 30,
                Location = "UpdatedLocation",
                Password = "UpdatedPassword2"
            };

            // Act
            var result = controller.Put(userId, body);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var updatedUser = Assert.IsType<Users>(okResult.Value);
            Assert.Equal(expectedUpdatedUser.Id, updatedUser.Id);
            Assert.Equal(expectedUpdatedUser.UserName, updatedUser.UserName);
            Assert.Equal(expectedUpdatedUser.Email, updatedUser.Email);
            Assert.Equal(expectedUpdatedUser.Age, updatedUser.Age);
            Assert.Equal(expectedUpdatedUser.Location, updatedUser.Location);
        }

        [Fact]
        public void PutUsers_UpdateUser_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            int userId = 1;
            var mockActionUsers = new Mock<IActionUsers>();
            var expectedUpdatedUser = new Users
            {
                Id = userId,
                UserName = "UpdatedUser",
                Email = "updated@example.com",
                Age = 30,
                Location = "UpdatedLocation"
            };
            mockActionUsers.Setup(x => x.UpdateUser(userId, It.IsAny<RequestUserBody>())).Returns(expectedUpdatedUser);

            var controller = new UsersController(mockActionUsers.Object);

            var body = new RequestUserBody
            {
                UserName = "UpdatedUser",
                Email = "updated@example.com",
                Age = 17,
                Location = "UpdatedLocation",
                Password = "UpdatedPassword2"
            };

            // Act
            var result = controller.Put(userId, body);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var updatedUser = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(updatedUser, "Age must be between 18 and 120\r\n");
        }

        [Fact]
        public void DeleteUsers_DeleteUser_ReturnsOkResultWithMessage()
        {
            // Arrange
            int userId = 1;
            string message = "User successfully deleted!";
            var mockActionUsers = new Mock<IActionUsers>();
            mockActionUsers.Setup(x => x.DeleteUser(userId)).Returns(message);
            var controller = new UsersController(mockActionUsers.Object);

            // Act
            var result = controller.Delete(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var resultMessage = Assert.IsType<string>(okResult.Value);
            Assert.Equal(message, resultMessage);
        }

        [Fact]
        public void DeleteUsers_DeleteUser_ReturnsBadRequestWithErrorMessage()
        {
            // Arrange
            int userId = 1;
            string message = "User not found";
            var mockActionUsers = new Mock<IActionUsers>();
            mockActionUsers.Setup(x => x.DeleteUser(userId)).Returns(message);
            var controller = new UsersController(mockActionUsers.Object);

            // Act
            var result = controller.Delete(userId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);

            var resultMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(message, resultMessage);
        }
    }
}
