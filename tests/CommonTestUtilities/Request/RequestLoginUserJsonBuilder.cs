using Bogus;
using CashFlow.Comunication.Requests.Users;

namespace CommonTestUtilities.Request
{
    public static class RequestLoginUserJsonBuilder
    {
        public static RequestLoginUserJson Build()
        {
            return new Faker<RequestLoginUserJson>()
                .RuleFor(t => t.Email, f => f.Internet.Email())
                .RuleFor(t => t.Password, f => f.Internet.Password(prefix: "Aa1!"));
        }
    }
}
