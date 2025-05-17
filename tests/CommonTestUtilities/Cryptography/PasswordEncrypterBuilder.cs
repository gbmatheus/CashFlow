using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public static class PasswordEncrypterBuilder
    {
        public static IPasswordEncrypter Build()
        {
            var mock = new Mock<IPasswordEncrypter>();
            mock.Setup(passwordEncrypter => passwordEncrypter.Encrypter(It.IsAny<string>())).Returns("39U6mcmKKtpj");
            return mock.Object;
        }
    }
}
