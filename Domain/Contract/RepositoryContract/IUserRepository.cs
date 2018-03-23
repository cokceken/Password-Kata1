using Password.Domain.Model;

namespace Password.Domain.Contract.RepositoryContract
{
    public interface IUserRepository : IRepository<User, int>
    {
        User GetWithUsername(string username);
        User GetWithEmail(string email);
    }
}
