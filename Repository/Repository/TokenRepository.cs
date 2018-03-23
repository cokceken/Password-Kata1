using System.Linq;
using NHibernate;
using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Model;
using Password.Repository.NHibernate;

namespace Password.Repository.Repository
{
    public class TokenRepository : Repository<Token, int>, ITokenRepository
    {
        public Token GetByValue(string value)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<Token>().FirstOrDefault(x => x.Value.Equals(value));
                NHibernateUtil.Initialize(result?.User);

                return result;
            }
        }

        public Token GetByUserId(int userId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<Token>().FirstOrDefault(x => x.User.Id == userId);
                NHibernateUtil.Initialize(result?.User);

                return result;
            }
        }
    }
}