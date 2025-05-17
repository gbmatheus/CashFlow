using Bogus;
using CashFlow.Comunication.Enums;
using CashFlow.Comunication.Requests.Expenses;

namespace CommonTestUtilities.Request
{
    public class RequestExpenseJsonBuilder
    {
        public static RequestExpenseJson Build()
        {
            return new Faker<RequestExpenseJson>()
                .RuleFor(t => t.Title, faker => faker.Commerce.ProductName())
                .RuleFor(t => t.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(t => t.Date, faker => faker.Date.Past())
                .RuleFor(t => t.Amount, faker => faker.Random.Decimal(min: 1, max: 100))
                .RuleFor(t => t.PaymentType, faker => faker.PickRandom<PaymentType>());
        }
    }
}
