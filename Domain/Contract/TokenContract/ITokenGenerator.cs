using Password.Domain.Model;

namespace Password.Domain.Contract.TokenContract
{
    public interface ITokenGenerator
    {
        Token GenerateToken(int userId);
    }
}
