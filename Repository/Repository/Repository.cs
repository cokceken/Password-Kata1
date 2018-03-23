using System.Collections.Generic;
using System.Linq;
using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Model;
using Password.Repository.NHibernate;

namespace Password.Repository.Repository
{
    public abstract class Repository<T, TIdType> : IRepository<T, TIdType> where T : BaseModel<TIdType>
    {
        public T Add(T item)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.Save(item);
                return item;
            }
        }

        public void Delete(T item)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public T Get(TIdType id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<T>().ToList();
            }
        }

        public void Update(T item)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                }
            }
        }
    }
}