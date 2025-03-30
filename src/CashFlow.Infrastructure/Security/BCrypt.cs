using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net;

namespace CashFlow.Infrastructure.Security
{
    internal class BCrypt : IPasswordEncrypter
    {
        public string Encrypter(string password)
        {
            return BC.BCrypt.HashPassword(password);
        }
    }
}
