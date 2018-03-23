using Password.Domain.Contract.UserContract;
using Password.Domain.Model;
using Password.Domain.Model.Exception;

namespace Password.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDataService _userDataService;

        public UserService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public User GetUserWithUsername(string username)
        {
            var user = _userDataService.GetUserWithUsername(username);
            if (user == null)
            {
                throw new UserNotFoundException($"Token not found with username: {username}");
            }

            return user;
        }

        public User GetUserWithEmail(string email)
        {
            var user = _userDataService.GetUserWithEmail(email);
            if (user == null)
            {
                throw new UserNotFoundException($"Token not found with email: {email}");
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            _userDataService.UpdateUser(user);
        }
    }
}