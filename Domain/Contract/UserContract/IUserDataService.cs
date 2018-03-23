using System.Collections.Generic;
using Password.Domain.Model;

namespace Password.Domain.Contract.UserContract
{
    public interface IUserDataService
    {
        User GetUserWithUsername(string username);
        IEnumerable<User> GetAllUsers();
        User GetUserWithEmail(string email);
        User GetUser(int id);
        void UpdateUser(User user);
    }
}
