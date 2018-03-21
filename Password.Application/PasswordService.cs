using System;
using Password.Application.DTO.Request;
using Password.Application.DTO.Response;
using Password.Infrastructure.Contract;

namespace Password.Application
{
    public class PasswordService : IPasswordService
    {
        private readonly IUserCredentialDataService _userCredentialDataService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IEmailService _emailService;
        private readonly ITokenDataService _tokenDataService;
        private readonly ITokenCreator _tokenCreator;

        public PasswordService(IUserCredentialDataService userCredentialDataService,
            IPasswordHashService passwordHashService, IEmailService emailService, ITokenDataService tokenDataService,
            ITokenCreator tokenCreator)
        {
            _userCredentialDataService = userCredentialDataService;
            _passwordHashService = passwordHashService;
            _emailService = emailService;
            _tokenDataService = tokenDataService;
            _tokenCreator = tokenCreator;
        }

        public AreValidUserCredentialsResponse AreValidUserCredentials(AreValidUserCredentialsRequest request)
        {
            var response = new AreValidUserCredentialsResponse();
            var userCredential = _userCredentialDataService.GetUserCredentialWithUsername(request.Username);

            if (userCredential == null)
            {
                return response;
            }

            var hashedPassword = _passwordHashService.HashPassword(request.Password, userCredential.PasswordSalt);
            if (userCredential.Password.Equals(hashedPassword))
            {
                response.Result = true;
            }

            return response;
        }

        public SendResetEmailResponse SendResetEmail(SendResetEmailRequest request)
        {
            var response = new SendResetEmailResponse();
            var userCredential = _userCredentialDataService.GetUserCredentialWithEmail(request.EmailAddress);

            if (userCredential != null)
            {
                var passwordToken = _tokenCreator.CreateToken(userCredential.Id);

                var existingToken = _tokenDataService.GetPasswordTokenByUserId(passwordToken.UserCredential.Id);
                if (existingToken != null)
                {
                    _tokenDataService.Delete(existingToken);
                }

                _tokenDataService.InsertToken(passwordToken);
                _emailService.Send(passwordToken.ToString(), userCredential.Email);
                response.Result = true;
            }

            return response;
        }

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            var response = new ChangePasswordResponse();
            var passwordToken = _tokenDataService.GetPasswordToken(request.Token);

            if (passwordToken == null)
            {
                throw new Exception("Token does not exist");
            }

            if (passwordToken.ExpireDateTime < DateTime.Now)
            {
                throw new Exception($"Password chage token expired at: {passwordToken.ExpireDateTime}");
            }

            var userCredential = passwordToken.UserCredential;

            if (userCredential.Id != request.UserId)
            {
                throw new Exception("Wrong user");
            }

            userCredential.Password =
                _passwordHashService.HashPassword(request.NewPassword, userCredential.PasswordSalt);
            _userCredentialDataService.UpdateUserCredential(userCredential);

            response.Result = true;

            return response;
        }
    }
}