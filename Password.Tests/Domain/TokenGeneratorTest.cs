using System;
using System.Linq;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Password.Domain.Service;

namespace Password.Tests.Domain
{
    [TestClass]
    public class TokenGeneratorTest
    {
        private TokenGenerator _cut;
        private const string TokenCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int TimeMarginOfError = 5;

        [TestInitialize]
        public void TestSetup()
        {
            _cut = new TokenGenerator();
        }

        [TestMethod]
        public void TokenGeneratorCreatesTokenWithLenghtOf10()
        {
            var result = _cut.GenerateToken(0);

            Assert.IsTrue(result.Value.Length == 10);
        }

        [TestMethod]
        public void TokenGeneratorCreatesTokenWithDesiredCharacters()
        {
            var result = _cut.GenerateToken(0);

            Assert.IsTrue(result.Value.Where(x => !TokenCharacters.Contains(x)).IsNullOrEmpty());
        }

        [TestMethod]
        public void TokenGeneratorCreatesTokenWith1HourExpireTime()
        {
            var result = _cut.GenerateToken(0);

            var secondDifference = (result.ExpireDateTime - DateTime.Now).TotalSeconds;

            Assert.IsTrue(secondDifference > 3600 - TimeMarginOfError && secondDifference < 3600 + TimeMarginOfError);
        }
    }
}