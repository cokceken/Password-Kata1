using Password.Domain.Model;

namespace Password.Infrastructure.Contract
{
    public interface ITokenCreator
    {
        PasswordToken CreateToken(int userId);
    }
}
