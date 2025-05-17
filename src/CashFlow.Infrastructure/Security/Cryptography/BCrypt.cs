using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net;

namespace CashFlow.Infrastructure.Security.Cryptography
{
    internal class BCrypt : IPasswordEncrypter
    {
        public string Encrypter(string password)
        {
            return BC.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BC.BCrypt.Verify(password, hash);
        }
    }
}
