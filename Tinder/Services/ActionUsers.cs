using System.Text.RegularExpressions;
using Tinder.Models;

namespace Tinder.Services
{
    public static class ActionUsers
    {
        public static IEnumerable<Users> GetUsers()
        {
            using (Context db = new())
            {
                var allUsers = db.Users.ToList();
                return allUsers;
            };
        }

        public static Users GetUserById(int id)
        {
            using (Context db = new())
            {
                var userById = db.Users.FirstOrDefault(a => a.Id == id);
                return userById;
            }
        }

        public static Users CreatetUser(RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            using (Context db = new())
            {
                db.Users.AddRange(new Users { UserName = body.UserName, Email = body.Email, Age = body.Age, Location = body.Location, Password = body.Password });
                db.SaveChanges();

                var createdUser = db.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

                return createdUser;
            };
        }
        public static Users UpdateUser(int id, RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            using (Context db = new())
            {
                var updateUser = db.Users.FirstOrDefault(a => a.Id == id);

                updateUser.UserName = body.UserName;
                updateUser.Email = body.Email;
                updateUser.Age = body.Age;
                updateUser.Location = body.Location;
                updateUser.Password = body.Password;

                db.SaveChanges();

                var updatedUser = db.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

                return updatedUser;
            };
        }

        public static string DeleteUser(int id)
        {
            using (Context db = new())
            {
                var item = db.Users.Find(id);
                if (item != null)
                {
                    db.Users.Remove(item);
                    db.SaveChanges();
                    return "User successfully deleted!";
                }
                else
                {
                    return "User not found";
                }
            };
        }

        public static string Validation(RequestUserBody body)
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
