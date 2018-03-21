using Password.Domain.Model;

namespace Password.Repository.Contract
{
    public interface IUserCredentialRepository : IRepository<UserCredential, int>
    {
        UserCredential GetWithUsername(string username);
        UserCredential GetWithEmail(string email);
    }
}
