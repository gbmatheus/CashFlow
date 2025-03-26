using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpenseUpdateOnlyRepository
    {
        Task<Expense?> GetById(int id);
        void Update(Expense expense);
    }
}
