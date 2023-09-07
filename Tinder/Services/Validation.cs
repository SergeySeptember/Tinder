using System.Text;
using System.Text.RegularExpressions;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public static class Validation
    {
        public static string ValidationData(RequestUserBody body)
        {
            StringBuilder validInfo = new StringBuilder();

            if (string.IsNullOrWhiteSpace(body.UserName))
            {
                validInfo.AppendLine("Field \"userName\" is empty");
            }
            if (string.IsNullOrWhiteSpace(body.Email) || !EmailValidation(body.Email))
            {
                validInfo.AppendLine("Field \"email\" is empty or invalid");
            }
            if (string.IsNullOrWhiteSpace(body.Password) || !PasswordValidation(body.Password))
            {
                validInfo.AppendLine("Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20");
            }
            if (string.IsNullOrWhiteSpace(body.Location))
            {
                validInfo.AppendLine("Field \"location\" is empty");
            }
            if (body.Age < 18 || body.Age > 120)
            {
                validInfo.AppendLine("Age must be between 18 and 120");
            }

            string resultOfValid = validInfo.ToString();

            if (string.IsNullOrWhiteSpace(resultOfValid))
            {
                return "true";
            }
            else
            {
                return resultOfValid;
            }
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
