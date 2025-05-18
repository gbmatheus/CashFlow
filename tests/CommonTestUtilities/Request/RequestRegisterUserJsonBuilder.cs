using Bogus;
using CashFlow.Comunication.Requests.Users;

namespace CommonTestUtilities.Request
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Builder()
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(t => t.Name, f => f.Internet.UserName())
                .RuleFor(t => t.Email, f => f.Internet.Email())
                .RuleFor(t => t.Password, f => f.Internet.Password(prefix: "Aa1!"));
        }
    }
}
