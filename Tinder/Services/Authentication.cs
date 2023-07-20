using Tinder.Models.Requests;

namespace Tinder.Services
{
    public class Authentication
    {
        private readonly Context _context;

        public Authentication(Context context)
        {
            _context = context;
        }
        public bool AuthenticationUser(AuthenticationBody body)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Password == body.Password);
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
