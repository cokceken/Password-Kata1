using Password.Domain.Contract.TokenContract;
using Password.Domain.Model;
using Password.Domain.Model.Exception;

namespace Password.Domain.Service
{
    public class TokenService : ITokenService
    {
        private readonly ITokenDataService _tokenDataService;
        private readonly ITokenGenerator _tokenGenerator;

        public TokenService(ITokenGenerator tokenGenerator, ITokenDataService tokenDataService)
        {
            _tokenGenerator = tokenGenerator;
            _tokenDataService = tokenDataService;
        }

        public Token GetTokenByUserId(int userId)
        {
            var token = _tokenDataService.GetTokenByUserId(userId);
            if (token == null)
            {
                throw new TokenNotFoundException("$Token not found with userId:{userId}");
            }

            return token;
        }

        public void DeleteToken(Token token)
        {
            _tokenDataService.Delete(token);
        }

        public void InsertToken(Token token)
        {
            _tokenDataService.InsertToken(token);
        }

        public Token GenerateToken(int userId)
        {
            return _tokenGenerator.GenerateToken(userId);
        }

        public Token GetTokenByValue(string tokenValue)
        {
            var token = _tokenDataService.GetTokenByValue(tokenValue);
            if (token == null)
            {
                throw new TokenNotFoundException($"Token not found with token value:{tokenValue}");
            }

            return token;
        }
    }
}