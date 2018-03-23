using Password.Domain.Model;

namespace Password.Domain.Contract.TokenContract
{
    public interface ITokenService
    {
        Token GetTokenByUserId(int userId);
        void DeleteToken(Token token);
        void InsertToken(Token token);
        Token GenerateToken(int userId);
        Token GetTokenByValue(string token);
    }
}
