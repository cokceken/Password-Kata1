using Password.Domain.Model;

namespace Password.Domain.Contract.TokenContract
{
    public interface ITokenDataService
    {
        Token GetTokenByValue(string token);
        Token GetTokenByUserId(int userId);
        void InsertToken(Token token);
        void Delete(Token existingToken);
    }
}