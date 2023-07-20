using System.Text.RegularExpressions;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public static class Validation
    {
        public static string ValidationData(RequestUserBody body)
        {
            if (string.IsNullOrWhiteSpace(body.UserName))
            {
                return "Field \"userName\" is empty";
            }
            if (string.IsNullOrWhiteSpace(body.Email) || !EmailValidation(body.Email))
            {
                return "Field \"email\" is empty or invalid";
            }
            if (string.IsNullOrWhiteSpace(body.Password) || !PasswordValidation(body.Password))
            {
                return "Field \"password\" is empty or must password must contain numbers, signs and be between 6 and 20";
            }
            if (string.IsNullOrWhiteSpace(body.Location))
            {
                return "Field \"location\" is empty";
            }
            if (body.Age < 18 || body.Age > 120)
            {
                return "Age must be between 18 and 120";
            }

            return "true";
        }
        private static bool EmailValidation(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        private static bool PasswordValidation(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,20}$";
            Match isMatch = Regex.Match(password, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
