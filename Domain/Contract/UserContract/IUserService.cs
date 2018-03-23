using Password.Domain.Model;

namespace Password.Domain.Contract.UserContract
{
    public interface IUserService
    {
        User GetUserWithUsername(string username);
        User GetUserWithEmail(string email);
        void UpdateUser(User user);
    }
}