using System.Collections.Generic;
using Password.Domain.Model;

namespace Password.Infrastructure.Contract
{
    public interface IUserCredentialDataService
    {
        UserCredential GetUserCredentialWithUsername(string username);
        IEnumerable<UserCredential> GetAllUserCredentials();
        UserCredential GetUserCredentialWithEmail(string email);
        UserCredential GetUserCredential(int id);
        void UpdateUserCredential(UserCredential userCredential);
    }
}
