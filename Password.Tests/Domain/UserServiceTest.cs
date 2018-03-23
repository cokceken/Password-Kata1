using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Password.Domain.Contract.UserContract;
using Password.Domain.Model;
using Password.Domain.Model.Exception;
using Password.Domain.Service;

namespace Password.Tests.Domain
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _cut;
        private Mock<IUserDataService> _userDataServiceMock;

        [TestInitialize]
        public void TestSetup()
        {
            _userDataServiceMock = new Mock<IUserDataService>();
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUserWithEmailIdThrowsUserNotFoundExceptionWhenNotFound()
        {
            _cut = new UserService(_userDataServiceMock.Object);

            _cut.GetUserWithEmail("email");
        }

        [TestMethod]
        public void GetUserWithEmailWorksWhenFound()
        {
            _userDataServiceMock.Setup(x => x.GetUserWithEmail(It.IsAny<string>())).Returns(new User());
            _cut = new UserService(_userDataServiceMock.Object);

            var user = _cut.GetUserWithEmail("email");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUserWithUsernameThrowsUserNotFoundExceptionWhenNotFound()
        {
            _cut = new UserService(_userDataServiceMock.Object);

            _cut.GetUserWithUsername("username");
        }

        [TestMethod]
        public void GetuserByValueWorksWhenFound()
        {
            _userDataServiceMock.Setup(x => x.GetUserWithUsername(It.IsAny<string>())).Returns(new User());
            _cut = new UserService(_userDataServiceMock.Object);

            var user = _cut.GetUserWithUsername("username");

            Assert.IsNotNull(user);
        }
    }
}
