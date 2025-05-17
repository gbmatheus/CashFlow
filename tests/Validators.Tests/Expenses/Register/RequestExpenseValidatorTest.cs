using CashFlow.Application.UseCases.Expenses;
using CashFlow.Comunication.Enums;
using CashFlow.Comunication.Requests.Expenses;
using CashFlow.Exception;
using CommonTestUtilities.Request;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register
{
    public class RequestExpenseValidatorTest
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
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("            ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            // Arrange
            var validator = new ExpenseValidator();
            RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
            request.Title = title;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-5)]
        public void Error_Amount_Invalid(decimal amount)
        {
            // Arrange
            var validator = new ExpenseValidator();
            RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
            request.Amount = amount;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
        }

        [Fact]
        public void Error_Date_Future()
        {
            // Arrange
            var validator = new ExpenseValidator();
            RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            // Arrange
            var validator = new ExpenseValidator();
            RequestExpenseJson request = RequestExpenseJsonBuilder.Build();
            request.PaymentType = (PaymentType)256;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }
    }
}
