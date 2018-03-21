using System.Linq;
using NHibernate;
using Password.Domain.Model;
using Password.Repository.Contract;
using Password.Repository.NHibernate;

namespace Password.Repository.Repository
{
    public class PasswordTokenRepository : Repository<PasswordToken, int>, IPasswordTokenRepository
    {
        public PasswordToken GetByToken(string token)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<PasswordToken>().FirstOrDefault(x => x.Token.Equals(token));
                NHibernateUtil.Initialize(result?.UserCredential);

                return result;
            }
        }

        public PasswordToken GetByUserId(int userId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var result = session.Query<PasswordToken>().FirstOrDefault(x => x.UserCredential.Id == userId);
                NHibernateUtil.Initialize(result?.UserCredential);

                return result;
            }
        }
    }
}
