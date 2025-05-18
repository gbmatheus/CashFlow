using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CommonTestUtilities.Cryptography;

namespace CommonTestUtilities.Entities
{
    public static class UserBuilder
    {
        public static User Build()
        {
            IPasswordEncrypter passwordEncrypter = new PasswordEncrypterBuilder().Build();

            return new Faker<User>()
                .RuleFor(t => t.Id, () => 1)
                .RuleFor(t => t.Name, f => f.Internet.UserName())
                .RuleFor(t => t.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(t => t.Password, (_, u) => passwordEncrypter.Encrypter(u.Password))
                .RuleFor(t => t.UserIdentifier, () => Guid.NewGuid());
        }
    }
}
