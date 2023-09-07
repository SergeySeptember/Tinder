using Tinder.Models.Requests;
using Tinder.Models;

namespace Tinder.Interfaces
{
    public interface IActionUsers
    {
        List<Users> GetUsers();
        Users GetUserById(int id);
        Users CreatetUser(RequestUserBody body);
        Users UpdateUser(int id, RequestUserBody body);
        string DeleteUser(int id);
    }
}
