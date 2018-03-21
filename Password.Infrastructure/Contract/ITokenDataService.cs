using Password.Domain.Model;

namespace Password.Infrastructure.Contract
{
    public interface ITokenDataService
    {
        PasswordToken GetPasswordToken(string token);
        PasswordToken GetPasswordTokenByUserId(int userId);
        void InsertToken(PasswordToken token);
        void Delete(PasswordToken existingToken);
    }
}