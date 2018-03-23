using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Model;
using Password.Repository.NHibernate;

namespace Password.Repository.Repository
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public User GetWithUsername(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>().Where(x => x.Username == username).SingleOrDefault();
            }
        }

        public User GetWithEmail(string email)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>().Where(x => x.Email == email).SingleOrDefault();
            }
        }
    }
}