using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Password.Domain.Contract;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Contract.UserContract;
using Password.Domain.Model;
using Password.Domain.Model.Exception;
using Password.Domain.Service;

namespace Password.Tests.Domain
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        private AuthenticationService _cut;
        private Mock<IUserService> _userServiceMock;
        private Mock<IHashService> _hashServiceMock;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<ILogger> _loggerMock;

        [TestInitialize]
        public void TestSetup()
        {
            _userServiceMock = new Mock<IUserService>();
            _hashServiceMock = new Mock<IHashService>();
            _emailServiceMock = new Mock<IEmailService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger>();

            _cut = new AuthenticationService(_hashServiceMock.Object, _emailServiceMock.Object,
                _tokenServiceMock.Object, _userServiceMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void AreValidUserCredentialsReturnTrueWithCorrectData()
        {
            _userServiceMock.Setup(x => x.GetUserWithUsername(It.IsAny<string>())).Returns(new User()
            {
                Password = "hashed"
            });
            _hashServiceMock.Setup(x => x.Hash(It.IsAny<string>())).Returns("hashed");

            var result = _cut.AreValidUserCredentials("username", "password");

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(CredentialMismatchException))]
        public void AreValidUserCredentialsThrowsCredentialMismatchExceptionWithUncorrectDate()
        {
            _userServiceMock.Setup(x => x.GetUserWithUsername(It.IsAny<string>())).Returns(new User()
            {
                Password = "hashed"
            });
            _hashServiceMock.Setup(x => x.Hash(It.IsAny<string>())).Returns("notHashed");

            _cut.AreValidUserCredentials("username", "password");
        }

        [TestMethod]
        public void SendResetEmailInvokesEmailServiceSentWithCorrectData()
        {
            _userServiceMock.Setup(x => x.GetUserWithEmail(It.IsAny<string>())).Returns(new User());
            _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<int>())).Returns(new Token()
            {
                User = new User()
            });

            _cut.SendResetEmail("email");

            _emailServiceMock.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void ChangePasswordThrowsInvalidTokenExceptionWhenTokenIsExpired()
        {
            _tokenServiceMock.Setup(x => x.GetTokenByValue(It.IsAny<string>())).Returns(new Token()
            {
                ExpireDateTime = DateTime.MinValue,
                User = new User() {Id = 1}
            });

            _cut.ChangePassword(1, "token", "newPassword");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenException))]
        public void ChangePasswordThrowsInvalidTokenExceptionWhenUserIdMismatch()
        {
            _tokenServiceMock.Setup(x => x.GetTokenByValue(It.IsAny<string>())).Returns(new Token()
            {
                User = new User() {Id = 0},
                ExpireDateTime = DateTime.MaxValue
            });

            _cut.ChangePassword(1, "token", "newPassword");
        }

        [TestMethod]
        public void ChangePasswordInvokesUpdateUserWithCorrectData()
        {
            _tokenServiceMock.Setup(x => x.GetTokenByValue(It.IsAny<string>())).Returns(new Token()
            {
                User = new User() {Id = 1, PasswordSalt = ""},
                ExpireDateTime = DateTime.MaxValue
            });

            _cut.ChangePassword(1, "token", "newPassword");

            _userServiceMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void SaltPasswordAddsStrings()
        {
            Assert.Equals(_cut.SaltPassword("password", "salt"), "passwordsalt");
        }

        [TestMethod]
        public void GenerateUrlFromTokenGeneratesUrlToChangePasswordPage()
        {
            Assert.Equals(_cut.GenerateUrlFromToken(new Token()
            {
                Id = 0,
                User = new User() {Id = 1},
                ExpireDateTime = DateTime.MaxValue,
                Value = "tokenValue"
            }), "http://localhost:56120/ChangePassword/Load?token=tokenValue&userId=1");
        }
    }
}