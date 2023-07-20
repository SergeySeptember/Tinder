using Tinder.Models;
using Tinder.Models.Requests;

namespace Tinder.Services
{
    public class ActionUsers
    {
        private readonly Context _context;
        private readonly PasswordHashing _passwordhashing;
        public ActionUsers(Context context, PasswordHashing passwordhashing)
        {
            _context = context;
            _passwordhashing = passwordhashing;
        }
        public IEnumerable<Users> GetUsers()
        {
            var allUsers = _context.Users.ToList();
            return allUsers;
        }

        public Users GetUserById(int id)
        {
            var userById = _context.Users.FirstOrDefault(a => a.Id == id);
            return userById;
        }

        public Users CreatetUser(RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            _context.Users.AddRange(new Users
            {
                UserName = body.UserName,
                Email = body.Email,
                Age = body.Age,
                Location = body.Location,
                Password = _passwordhashing.HashPassword(body.Password)
            });
            _context.SaveChanges();

            var createdUser = _context.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

            return createdUser;
        }
        public Users UpdateUser(int id, RequestUserBody body)
        {
            body.UserName = body.UserName.Trim();
            body.Location = body.Location.Trim();

            var updateUser = _context.Users.FirstOrDefault(a => a.Id == id);

            updateUser.UserName = body.UserName;
            updateUser.Email = body.Email;
            updateUser.Age = body.Age;
            updateUser.Location = body.Location;
            updateUser.Password = _passwordhashing.HashPassword(body.Password);

            _context.SaveChanges();

            var updatedUser = _context.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Email == body.Email);

            return updatedUser;
        }

        public string DeleteUser(int id)
        {
            var item = _context.Users.Find(id);
            if (item != null)
            {
                _context.Users.Remove(item);
                _context.SaveChanges();
                return "User successfully deleted!";
            }
            else
            {
                return "User not found";
            }
        }
    }
}
