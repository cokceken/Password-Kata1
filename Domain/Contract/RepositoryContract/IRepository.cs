using System.Collections.Generic;
using Password.Domain.Model;

namespace Password.Domain.Contract.RepositoryContract
{
    public interface IRepository<T, in TIdType> where T : BaseModel<TIdType>
    {
        T Get(TIdType id);
        void Update(T item);
        T Add(T item);
        IEnumerable<T> GetAll();
        void Delete(T item);
    }
}
