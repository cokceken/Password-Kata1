using Password.Infrastructure.Contract;

namespace Password.Infrastructure.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly IHashService _hashService;

        public PasswordHashService(IHashService hashService)
        {
            _hashService = hashService;
        }

        public string SaltPassword(string password, string salt)
        {
            return password + salt;
        }

        public string HashPassword(string password, string salt)
        {
            var saltedPassword = SaltPassword(password, salt);
            return _hashService.HashPassword(saltedPassword);
        }
    }
}