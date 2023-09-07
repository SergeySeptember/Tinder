using Tinder.Models.Requests;
using Tinder.Services;

namespace UnitTests
{
    public class ValidationDataTest
    {
        [Fact]
        public void TestValidationData_Correct()
        {
            // Arrange
            RequestUserBody body = new() {UserName = "Seroja", Age = 18, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };
            
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
            Assert.Equal(result, "Field \"userName\" is empty\r\n");
        }

        [Fact]
        public void TestValidationData_AgeIsntCorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 17, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = 121, Email = "example@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };
            
            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);

            // Assert
            Assert.Equal(result1, "Age must be between 18 and 120\r\n");
            Assert.Equal(result2, "Age must be between 18 and 120\r\n");
        }

        [Fact]
        public void TestValidationData_EmailIsntCorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = "examplemail.ru", Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = 20, Email = "example@mailru", Location = "Moscow", Password = "Galaxytab2)" };
            RequestUserBody body3 = new() { UserName = "Seroja", Age = 20, Email = "123$@mail.ru", Location = "Moscow", Password = "Galaxytab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);
            string result3 = Validation.ValidationData(body3);

            // Assert
            Assert.Equal(result1, "Field \"email\" is empty or invalid\r\n");
            Assert.Equal(result2, "Field \"email\" is empty or invalid\r\n");
            Assert.Equal(result3, "Field \"email\" is empty or invalid\r\n");
        }

        [Fact]
        public void TestValidationData_LocationIsntCorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = " ", Password = "Galaxytab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);

            // Assert
            Assert.Equal(result1, "Field \"location\" is empty\r\n");
        }

        [Fact]
        public void TestValidationData_PasswordIsntCorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = "Moscow", Password = "Gab2)" };
            RequestUserBody body2 = new() { UserName = "Seroja", Age = 20, Email = "example@mail.ru", Location = "Moscow", Password = "galaxytab" };
            RequestUserBody body3 = new() { UserName = "Seroja", Age = 20, Email = "123@mail.ru", Location = "Moscow", Password = "Galaxytab" };

            // Act
            string result1 = Validation.ValidationData(body1);
            string result2 = Validation.ValidationData(body2);
            string result3 = Validation.ValidationData(body3);

            // Assert
            Assert.Equal(result1, "Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20\r\n");
            Assert.Equal(result2, "Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20\r\n");
            Assert.Equal(result3, "Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20\r\n");
        }

        [Fact]
        public void TestValidationData_AllDataIsIncorrect()
        {
            // Arrange
            RequestUserBody body1 = new() { UserName = " ", Age = 17, Email = "examplemail.ru", Location = " ", Password = "Gab2)" };

            // Act
            string result1 = Validation.ValidationData(body1);

            // Assert
            Assert.Equal(result1, "Field \"userName\" is empty\r\nField \"email\" is empty or invalid\r\nField \"password\" is empty or must password must contain numbers, signs and be between 6 and 20\r\nField \"location\" is empty\r\nAge must be between 18 and 120\r\n");
        }
    }
}
