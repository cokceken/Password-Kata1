using System;
using Password.Domain.Contract;
using Password.Domain.Contract.AuthenticationContract;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Contract.UserContract;
using Password.Domain.Model.Exception;

namespace Password.Domain.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly ILogger _logger;

        public AuthenticationService(IHashService hashService, IEmailService emailService, ITokenService tokenService,
            IUserService userService, ILogger logger)
        {
            _hashService = hashService;
            _emailService = emailService;
            _tokenService = tokenService;
            _userService = userService;
            _logger = logger;
        }

        public bool AreValidUserCredentials(string username, string password)
        {
            var userCredential = _userService.GetUserWithUsername(username);

            if (userCredential == null)
            {
                return false;
            }

            var hashedPassword = _hashService.Hash(SaltPassword(password, userCredential.PasswordSalt));
            return userCredential.Password.Equals(hashedPassword);
        }

        public void SendResetEmail(string email)
        {
            var user = _userService.GetUserWithEmail(email);
            var passwordToken = _tokenService.GenerateToken(user.Id);

            try
            {
                var existingToken = _tokenService.GetTokenByUserId(passwordToken.User.Id);
                _tokenService.DeleteToken(existingToken);
            }
            catch (TokenNotFoundException e)
            {
                _logger.Debug($"Token not found for userId: {passwordToken.User.Id}", e);
            }

            _tokenService.InsertToken(passwordToken);
            _emailService.Send(passwordToken.ToString(), user.Email);
        }

        public void ChangePassword(int userId, string token, string newPassword)
        {
            var passwordToken = _tokenService.GetTokenByValue(token);

            if (passwordToken.ExpireDateTime < DateTime.Now)
            {
                throw new InvalidTokenException($"Password chage token expired at: {passwordToken.ExpireDateTime}");
            }

            var user = passwordToken.User;

            if (user.Id != userId)
            {
                throw new InvalidTokenException("Wrong user");
            }

            user.Password =
                _hashService.Hash(SaltPassword(newPassword, user.PasswordSalt));
            _userService.UpdateUser(user);
        }

        private string SaltPassword(string password, string salt)
        {
            return password + salt;
        }
    }
}