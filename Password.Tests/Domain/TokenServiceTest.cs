using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Password.Domain.Contract.TokenContract;
using Password.Domain.Model;
using Password.Domain.Model.Exception;
using Password.Domain.Service;

namespace Password.Tests.Domain
{
    [TestClass]
    public class TokenServiceTest
    {
        private TokenService _cut;
        private Mock<ITokenGenerator> _tokenGeneratorMock;
        private Mock<ITokenDataService> _tokenDataServiceMock;

        [TestInitialize]
        public void TestSetup()
        {
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _tokenDataServiceMock = new Mock<ITokenDataService>();
            _cut = new TokenService(_tokenGeneratorMock.Object, _tokenDataServiceMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(TokenNotFoundException))]
        public void GetTokenByUserIdThrowsTokenNotFoundExceptionWhenNotFound()
        {
            _cut.GetTokenByUserId(1);
        }

        [TestMethod]
        public void GetTokenByUserIdWorksWhenFound()
        {
            _tokenDataServiceMock.Setup(x => x.GetTokenByUserId(It.IsAny<int>())).Returns(new Token());

            var token = _cut.GetTokenByUserId(1);

            Assert.IsNotNull(token);
        }

        [TestMethod]
        [ExpectedException(typeof(TokenNotFoundException))]
        public void GetTokenByValueThrowsTokenNotFoundExceptionWhenNotFound()
        {
            _cut.GetTokenByValue("any");
        }

        [TestMethod]
        public void GetTokenByValueWorksWhenFound()
        {
            _tokenDataServiceMock.Setup(x => x.GetTokenByValue(It.IsAny<string>())).Returns(new Token());

            var token = _cut.GetTokenByValue("any");

            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void GenerateTokenWorks()
        {
            _tokenGeneratorMock.Setup(x => x.GenerateToken(It.IsAny<int>())).Returns(new Token());

            var token = _cut.GenerateToken(1);

            Assert.IsNotNull(token);
        }
    }
}