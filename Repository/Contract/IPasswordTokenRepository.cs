using Password.Domain.Model;

namespace Password.Repository.Contract
{
    public interface IPasswordTokenRepository : IRepository<PasswordToken, int>
    {
        PasswordToken GetByToken(string token);
        PasswordToken GetByUserId(int userId);
    }
}