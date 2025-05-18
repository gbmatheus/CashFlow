using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncrypterBuilder
    {
        Mock<IPasswordEncrypter> _passwordEncrypter;

        public PasswordEncrypterBuilder()
        {
            _passwordEncrypter = new Mock<IPasswordEncrypter>();
            _passwordEncrypter.Setup(
               passwordEncrypter => passwordEncrypter.Encrypter(It.IsAny<string>())
            ).Returns("39U6mcmKKtpj");
        }

        public PasswordEncrypterBuilder Verify(string password)
        {
            _passwordEncrypter.Setup(config => config.Verify(password, It.IsAny<string>())).Returns(true);
            return this;
        }

        public IPasswordEncrypter Build()
        {
            return _passwordEncrypter.Object;
        }
    }
}
