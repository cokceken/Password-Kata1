using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Password.Domain.Contract;

namespace Password.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string Hash(string value)
        {
            var crypt = new SHA256Managed();
            var hash = string.Empty;
            var crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(value));

            return crypto.Aggregate(hash, (current, theByte) => current + theByte.ToString("x2"));
        }
    }
}