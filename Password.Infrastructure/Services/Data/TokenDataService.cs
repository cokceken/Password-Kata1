using Password.Domain.Contract.RepositoryContract;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Model;

namespace Password.Infrastructure.Services.Data
{
    public class TokenDataService : ITokenDataService
    {
        private readonly ITokenRepository _repository;

        public TokenDataService(ITokenRepository repository)
        {
            _repository = repository;
        }

        public Token GetTokenByValue(string token)
        {
            return _repository.GetByValue(token);
        }

        public Token GetTokenByUserId(int userId)
        {
            return _repository.GetByUserId(userId);
        }

        public void InsertToken(Token token)
        {
            _repository.Add(token);
        }

        public void Delete(Token existingToken)
        {
            _repository.Delete(existingToken);
        }
    }
}