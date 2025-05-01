using CashFlow.Application.UseCases.Expenses;
using CashFlow.Comunication.Enums;
using CashFlow.Comunication.Requests.Expenses;
using CommonTestUtilities.Request;

namespace Validators.Tests.Expenses.Register
{
    public class RequestExpenseValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            // Triple A - Arrange, Act, Assert
            // Arrange - Cria as instâncias e prepara os dados necessário para fazer a execução do teste
            RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
            var validator = new ExpenseValidator();

            // Act - Realiza a operação/situação que será testada
            var result = validator.Validate(request);

            // Assert - Valida o resultado
            Assert.True(result.IsValid);
        }
    }
}
