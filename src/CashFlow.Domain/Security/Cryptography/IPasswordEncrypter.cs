namespace CashFlow.Domain.Security.Cryptography
{
    public interface IPasswordEncrypter
    {
        string Encrypter(string password);
    }
}
