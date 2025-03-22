using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpenseRepository : IExpenseRepository
    {
        public void Add(Expense expense)
        {
            var dbContext = new CashFlowDBContext();

            dbContext.Expenses.Add(expense);

            dbContext.SaveChanges();
        }
    }
}
