using System.Collections.Generic;
using Password.Domain.Model;
using Password.Infrastructure.Contract;
using Password.Repository.Contract;

namespace Password.Infrastructure.Services.Data
{
    public class UserCredentialDataService : IUserCredentialDataService
    {
        private readonly IUserCredentialRepository _userCredentialRepository;

        public UserCredentialDataService(IUserCredentialRepository userCredentialRepository)
        {
            _userCredentialRepository = userCredentialRepository;
        }

        public UserCredential GetUserCredentialWithUsername(string username)
        {
            return _userCredentialRepository.GetWithUsername(username);
        }

        public IEnumerable<UserCredential> GetAllUserCredentials()
        {
            return _userCredentialRepository.GetAll();
        }

        public UserCredential GetUserCredentialWithEmail(string email)
        {
            return _userCredentialRepository.GetWithEmail(email);
        }

        public UserCredential GetUserCredential(int id)
        {
            return _userCredentialRepository.Get(id);
        }

        public void UpdateUserCredential(UserCredential userCredential)
        {
            _userCredentialRepository.Update(userCredential);
        }
    }
}