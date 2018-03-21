using System;
using System.Linq;
using Password.Domain.Model;
using Password.Infrastructure.Contract;

namespace Password.Infrastructure.Services
{
    public class TokenCreator : ITokenCreator
    {
        public PasswordToken CreateToken(int userId)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var token = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return new PasswordToken()
            {
                Token = token,
                ExpireDateTime = DateTime.Now.AddHours(1),
                UserCredential = new UserCredential() {Id = userId}
            };
        }
    }
}