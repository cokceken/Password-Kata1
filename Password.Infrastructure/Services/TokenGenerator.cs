using System;
using System.Linq;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Model;

namespace Password.Infrastructure.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        public Token GenerateToken(int userId)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var token = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return new Token()
            {
                Value = token,
                ExpireDateTime = DateTime.Now.AddHours(1),
                User = new User() {Id = userId}
            };
        }
    }
}