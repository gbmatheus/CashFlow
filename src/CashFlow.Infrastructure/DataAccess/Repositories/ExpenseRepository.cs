using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpenseRepository : IExpenseReadOnlyRepository,IExpenseWriteOnlyRepository
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

        public async Task<List<Expense>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        public async Task<Expense?> GetById(int id)
        {
            return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.Id == id).FirstOrDefaultAsync();
        }
    }
}
