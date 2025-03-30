using CashFlow.Comunication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public interface IGetAllExpenseUseCase
    {
        Task<ResponseExpensesJson> Execute();
    }
}
