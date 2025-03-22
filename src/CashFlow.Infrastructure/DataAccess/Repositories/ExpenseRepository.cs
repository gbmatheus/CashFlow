using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpenseRepository : IExpenseRepository
    {
        private CashFlowDBContext _dbContext;

        public ExpenseRepository(CashFlowDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }
    }
}
