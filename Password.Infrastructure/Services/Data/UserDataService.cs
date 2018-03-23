using System.Collections.Generic;
using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Contract.UserContract;
using Password.Domain.Model;

namespace Password.Infrastructure.Services.Data
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserRepository _userRepository;

        public UserDataService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserWithUsername(string username)
        {
            return _userRepository.GetWithUsername(username);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserWithEmail(string email)
        {
            return _userRepository.GetWithEmail(email);
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}