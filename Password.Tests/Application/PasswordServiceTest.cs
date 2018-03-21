using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Password.Application;
using Password.Application.DTO.Request;
using Password.Domain.Model;
using Password.Infrastructure.Contract;

namespace Password.Tests.Application
{
    [TestClass]
    public class PasswordServiceTest
    {
        [TestMethod]
        public void AreValidUserCredentialsWorksWithCorrectData()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new AreValidUserCredentialsRequest()
            {
                Password = "password",
                Username = "username"
            };

            mockUserCredentialDataService.Setup(x => x.GetUserCredentialWithUsername(It.IsAny<string>())).Returns(
                new UserCredential()
                {
                    Password = "hashedPassword",
                    Email = "email",
                    PasswordSalt = "salt",
                    Username = "username",
                    Id = 1
                });

            mockPasswordHashService.Setup(x =>
                    x.HashPassword(It.Is<string>(s => s.Equals("password")), It.Is<string>(s => s.Equals("salt"))))
                .Returns("hashedPassword");

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.AreValidUserCredentials(request);

            // Assert
            Assert.IsTrue(response.Result);
        }

        [TestMethod]
        public void AreValidUserCredentialsDoesNotWorkWithWrongPassword()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new AreValidUserCredentialsRequest()
            {
                Password = "wrongPassword",
                Username = "username"
            };

            mockUserCredentialDataService.Setup(x => x.GetUserCredentialWithUsername(It.IsAny<string>())).Returns(
                new UserCredential()
                {
                    Password = "hashedpassword",
                    Email = "email",
                    PasswordSalt = "salt",
                    Username = "username"
                });

            mockPasswordHashService.Setup(x =>
                    x.HashPassword(It.Is<string>(s => s.Equals("wrongPassword")), It.Is<string>(s => s.Equals("salt"))))
                .Returns("wrongHashedPassword");

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.AreValidUserCredentials(request);

            // Assert
            Assert.IsFalse(response.Result);
        }

        [TestMethod]
        public void AreValidUserCredentialsDoesNotWorkWithNonExistingUser()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new AreValidUserCredentialsRequest()
            {
                Password = "password",
                Username = "wrongUsername"
            };

            mockUserCredentialDataService.Setup(x => x.GetUserCredentialWithUsername(It.IsAny<string>()))
                .Returns(null as UserCredential);

            mockPasswordHashService.Setup(x =>
                    x.HashPassword(It.Is<string>(s => s.Equals("password")), It.Is<string>(s => s.Equals("salt"))))
                .Returns("hashedPassword");

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.AreValidUserCredentials(request);

            // Assert
            Assert.IsFalse(response.Result);
        }

        [TestMethod]
        public void SendResetEmailWorksWithCorrectData()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new SendResetEmailRequest()
            {
                EmailAddress = "email"
            };

            mockUserCredentialDataService
                .Setup(x => x.GetUserCredentialWithEmail(It.Is<string>(s => s.Equals("email"))))
                .Returns(new UserCredential()
                {
                    Password = "password",
                    PasswordSalt = "salt",
                    Email = "email",
                    Id = 1,
                    Username = "username"
                });

            mockTokenCreator.Setup(x => x.CreateToken(It.Is<int>(i => i == 1))).Returns(new PasswordToken()
            {
                UserCredential = new UserCredential() {Id = 1},
                ExpireDateTime = DateTime.MaxValue,
                Id = 1,
                Token = "token"
            });

            mockPasswordHashService.Setup(x =>
                    x.HashPassword(It.Is<string>(s => s.Equals("password")), It.Is<string>(s => s.Equals("salt"))))
                .Returns("hashedPassword");

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.SendResetEmail(request);

            // Assert
            Assert.IsTrue(response.Result);
        }

        [TestMethod]
        public void SendResetEmailDoesNotWorkWithNonExistingEmail()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new SendResetEmailRequest()
            {
                EmailAddress = "wrongEmail"
            };

            mockUserCredentialDataService
                .Setup(x => x.GetUserCredentialWithEmail(It.Is<string>(s => s.Equals("wrongEmail"))))
                .Returns(null as UserCredential);

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.SendResetEmail(request);

            // Assert
            Assert.IsFalse(response.Result);
        }

        [TestMethod]
        public void ChangePasswordWorksWithCorrectData()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new ChangePasswordRequest()
            {
                UserId = 1,
                Token = "token",
                NewPassword = "newPassword"
            };

            mockTokenDataService.Setup(x => x.GetPasswordToken(It.Is<string>(s => s.Equals("token")))).Returns(
                new PasswordToken()
                {
                    UserCredential = new UserCredential() { Id = 1 },
                    Token = "token",
                    Id = 1,
                    ExpireDateTime = DateTime.MaxValue
                });

            mockUserCredentialDataService.Setup(x => x.GetUserCredential(It.Is<int>(i => i == 1)))
                .Returns(new UserCredential()
                {
                    Password = "password",
                    PasswordSalt = "salt",
                    Email = "email",
                    Id = 1,
                    Username = "username"
                });

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.ChangePassword(request);

            // Assert
            Assert.IsTrue(response.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Password chage token expired at: 1/1/0001 12:00:00 AM")]
        public void ChangePasswordDoesNotWorkWithExpiredToken()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new ChangePasswordRequest()
            {
                UserId = 1,
                Token = "token",
                NewPassword = "newPassword"
            };

            mockTokenDataService.Setup(x => x.GetPasswordToken(It.Is<string>(s => s.Equals("token")))).Returns(
                new PasswordToken()
                {
                    UserCredential = new UserCredential() { Id = 1 },
                    Token = "token",
                    Id = 1,
                    ExpireDateTime = DateTime.MinValue
                });

            mockUserCredentialDataService.Setup(x => x.GetUserCredential(It.Is<int>(i => i == 1)))
                .Returns(new UserCredential()
                {
                    Password = "password",
                    PasswordSalt = "salt",
                    Email = "email",
                    Id = 1,
                    Username = "username"
                });

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.ChangePassword(request);

            // Assert
            Assert.Fail("Expected token expired exception");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wrong user")]
        public void ChangePasswordDoesNotWorkWithWrongUser()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new ChangePasswordRequest()
            {
                UserId = 1,
                Token = "token",
                NewPassword = "newPassword"
            };

            mockTokenDataService.Setup(x => x.GetPasswordToken(It.Is<string>(s => s.Equals("token")))).Returns(
                new PasswordToken()
                {
                    UserCredential = new UserCredential() { Id = 2 },
                    Token = "token",
                    Id = 1,
                    ExpireDateTime = DateTime.MaxValue
                });

            mockUserCredentialDataService.Setup(x => x.GetUserCredential(It.Is<int>(i => i == 2)))
                .Returns(new UserCredential()
                {
                    Password = "password",
                    PasswordSalt = "salt",
                    Email = "email",
                    Id = 2,
                    Username = "username"
                });

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.ChangePassword(request);

            // Assert
            Assert.Fail("Expected wrong user exception");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Token does not exist")]
        public void ChangePasswordDoesNotWorkWithNonExistingToken()
        {
            // Arrange
            var mockUserCredentialDataService = new Mock<IUserCredentialDataService>();
            var mockPasswordHashService = new Mock<IPasswordHashService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockTokenDataService = new Mock<ITokenDataService>();
            var mockTokenCreator = new Mock<ITokenCreator>();

            var request = new ChangePasswordRequest()
            {
                UserId = 1,
                Token = "token",
                NewPassword = "newPassword"
            };

            mockTokenDataService.Setup(x => x.GetPasswordToken(It.Is<string>(s => s.Equals("token"))))
                .Returns(null as PasswordToken);

            var functionUnderTest = new PasswordService(mockUserCredentialDataService.Object,
                mockPasswordHashService.Object,
                mockEmailService.Object, mockTokenDataService.Object, mockTokenCreator.Object);

            // Act
            var response = functionUnderTest.ChangePassword(request);

            // Assert
            Assert.Fail("Expected token does not exist exception");
        }
    }
}