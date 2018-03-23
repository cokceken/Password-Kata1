using Password.Domain.Model;

namespace Password.Domain.Contract.RepositoryContract
{
    public interface ITokenRepository : IRepository<Token, int>
    {
        Token GetByValue(string value);
        Token GetByUserId(int userId);
    }
}