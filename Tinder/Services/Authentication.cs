using Tinder.Models.Requests;

namespace Tinder.Services
{
    public static class Authentication
    {
        public static bool AuthenticationUser(AuthenticationBody body)
        {
            using (Context db = new())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == body.UserName && u.Password == body.Password);
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
}
