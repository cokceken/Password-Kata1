using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Model;

namespace Password.Repository.Repository
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public User GetWithUsername(string username)
        {
            return Session.QueryOver<User>().Where(x => x.Username == username)
                .SingleOrDefault();
        }

        public User GetWithEmail(string email)
        {
            return Session.QueryOver<User>().Where(x => x.Email == email).SingleOrDefault();
        }
    }
}