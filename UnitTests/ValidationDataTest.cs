using AutoFixture;
using Tinder.Models.Requests;
using Tinder.Services;

namespace UnitTests
{
    public class ValidationDataTest
    {
        [Fact]
        public void TestValidationData_AllCorrect()
        {
            // Arrange
            var body = new Fixture().Build<RequestUserBody>()
                .With(x => x.Age, 50)
                .With(x => x.Email, "example@mail.ru")
                .With(x => x.Password, "Galaxytab2")
                .Create();

            // Act
            string result = Validation.ValidationData(body);

            // Assert
            Assert.Equal(result, "true");
        }

        [Fact]
        public void TestValidationData_NameIsntCorrect()
        {
            // Arrange
            RequestUserBody body = new() { UserName = " ", Age = 18, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };

            // Act
            string result = Validation.ValidationData(body);

            // Assert
            Assert.Equal(result, Validation.errorName);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(121)]
        public void TestValidationData_AgeIsntCorrect(int age)
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = age, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = age, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);

            // Assert
            Assert.Equal(result1, Validation.errorAge);
            Assert.Equal(result2, Validation.errorAge);
        }



        [Theory]
        [InlineData("examplemail.ru")]
        [InlineData("example@mailru")]
        [InlineData("123$@mail.ru")]
        public void TestValidationData_EmailIsntCorrect(string email)
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = email, Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = 20, Email = email, Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body3 = new() { UserName = "Seroja", Age = 20, Email = email, Location = "Moscow", Password = "Galaxytab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);
            string result3 = Validation.ValidationData(body3);

            // Assert
            Assert.Equal(result1, Validation.errorEmail);
            Assert.Equal(result2, Validation.errorEmail);
            Assert.Equal(result3, Validation.errorEmail);
        }

        [Fact]
        public void TestValidationData_LocationIsntCorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = " ", Password = "Galaxytab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);

            // Assert
            Assert.Equal(result1, Validation.errorLocation);
        }

        [Theory]
        [InlineData("Gab2)")]
        [InlineData("galaxytab")]
        [InlineData("Galaxytab")]
        public void TestValidationData_PasswordIsntCorrect(string password)
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = "Moscow", Password = password };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = "Moscow", Password = password };
            RequestUserBody body3 = new() { UserName = "Seroja", Age = 20, Email = "123@mail.ru", Location = "Moscow", Password = password };

            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);
            string result3 = Validation.ValidationData(body3);

            // Assert
            Assert.Equal(result1, Validation.errorPassword);
            Assert.Equal(result2, Validation.errorPassword);
            Assert.Equal(result3, Validation.errorPassword);
        }

        [Fact]
        public void TestValidationData_AllDataIsIncorrect()
        {
            // Arrange  
            RequestUserBody body1 = new() { UserName = " ", Age = 17, Email = "examplemail.ru", Location = " ", Password = "Gab2)" };
            string expectedAnswer = Validation.errorName + Validation.errorEmail + Validation.errorPassword + Validation.errorLocation + Validation.errorAge;

            // Act
            string result = Validation.ValidationData(body1);

            // Assert
            Assert.Equal(result, expectedAnswer);
        }
    }
}
