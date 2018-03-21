using Password.Domain.Model;
using Password.Infrastructure.Contract;
using Password.Repository.Contract;

namespace Password.Infrastructure.Services.Data
{
    public class TokenDataService : ITokenDataService
    {
        private readonly IPasswordTokenRepository _tokenRepository;

        public TokenDataService(IPasswordTokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public PasswordToken GetPasswordToken(string token)
        {
            return _tokenRepository.GetByToken(token);
        }

        public PasswordToken GetPasswordTokenByUserId(int userId)
        {
            return _tokenRepository.GetByUserId(userId);
        }

        public void InsertToken(PasswordToken token)
        {
            _tokenRepository.Add(token);
        }

        public void Delete(PasswordToken existingToken)
        {
            _tokenRepository.Delete(existingToken);
        }
    }
}