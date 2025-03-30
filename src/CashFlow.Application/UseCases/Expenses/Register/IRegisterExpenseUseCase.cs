using CashFlow.Comunication.Requests.Expenses;
using CashFlow.Comunication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request);
    }
}
