using System.Text;
using System.Text.RegularExpressions;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public static class Validation
    {
        public static readonly string errorName = "Field \"userName\" is empty\r\n";
        public static readonly string errorEmail = "Field \"email\" is empty or invalid\r\n";
        public static readonly string errorPassword = "Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20\r\n";
        public static readonly string errorLocation = "Field \"location\" is empty\r\n";
        public static readonly string errorAge = "Age must be between 18 and 120\r\n";


        public static string ValidationData(RequestUserBody body)
        {
            StringBuilder validInfo = new StringBuilder();

            if (string.IsNullOrWhiteSpace(body.UserName))
                validInfo.Append(errorName);

            if (string.IsNullOrWhiteSpace(body.Email) || !EmailValidation(body.Email))
                validInfo.Append(errorEmail);

            if (string.IsNullOrWhiteSpace(body.Password) || !PasswordValidation(body.Password))
                validInfo.Append(errorPassword);

            if (string.IsNullOrWhiteSpace(body.Location))
                validInfo.Append(errorLocation);

            if (body.Age < 18 || body.Age > 120)
                validInfo.Append(errorAge);

            string resultOfValid = validInfo.ToString();

            if (string.IsNullOrWhiteSpace(resultOfValid))
                return "true";
            else
                return resultOfValid.ToString();
        }
        private static bool EmailValidation(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        private static bool PasswordValidation(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
