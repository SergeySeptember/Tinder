using Microsoft.AspNetCore.Mvc;
using Tinder.Models.Requests;
using Moq;
using Tinder.Controllers;
using Tinder.Interfaces;
using Tinder.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
            var result = controller.Get();

            // Assert
            //Assert.Equal(expectedUsers.Count, result.Count());
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
        public void CreatetUser_CreatesUserAndReturnIt()
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
                    Location = "Бомж",
                    Password = "Galaxytab2)"
                };

                // Act
                var createdUser = actionUsers.CreatetUser(requestUserBody);

                // Assert
                Assert.NotNull(createdUser);
                Assert.Equal(requestUserBody.UserName, createdUser.UserName);
                Assert.Equal(requestUserBody.Email, createdUser.Email);
                Assert.Equal(requestUserBody.Age, createdUser.Age);
                Assert.Equal("Test City", createdUser.Location);
                // Check that the password is hashed
                Assert.NotEqual("test_password", createdUser.Password);
            }

            using (var context = new Context(options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
