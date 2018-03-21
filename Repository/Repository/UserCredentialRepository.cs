using Password.Domain.Model;
using Password.Repository.Contract;
using Password.Repository.NHibernate;

namespace Password.Repository.Repository
{
    public class UserCredentialRepository : Repository<UserCredential, int>, IUserCredentialRepository
    {
        public UserCredential GetWithUsername(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<UserCredential>().Where(x => x.Username == username).SingleOrDefault();
            }
        }

        public UserCredential GetWithEmail(string email)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<UserCredential>().Where(x => x.Email == email).SingleOrDefault();
            }
        }
    }
}