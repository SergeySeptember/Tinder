using Tinder.Models.Requests;

namespace Tinder.Services
{
    public class Authentication
    {
        private readonly Context _context;
        private readonly PasswordHashing _passwordhashing;
        public Authentication(Context context, PasswordHashing passwordhashing)
        {
            _context = context;
            _passwordhashing = passwordhashing;
        }
        public bool AuthenticationUser(AuthenticationBody body)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Password == _passwordhashing.HashPassword(body.Password));
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
