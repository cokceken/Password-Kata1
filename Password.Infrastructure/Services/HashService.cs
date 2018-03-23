using Password.Domain.Contract;

namespace Password.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string Hash(string value)
        {
            return value;
        }
    }
}