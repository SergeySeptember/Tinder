using System.Security.Cryptography;
using System.Text;

namespace Tinder.Services
{
    public class PasswordHashing
    {
        public string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var item in hash)
            {
                sb.Append(item.ToString("X2"));
            }

            return Convert.ToString(sb);
        }
    }
}
