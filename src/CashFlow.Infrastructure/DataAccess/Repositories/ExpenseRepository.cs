using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpenseRepository : IExpenseReadOnlyRepository, IExpenseWriteOnlyRepository, IExpenseUpdateOnlyRepository
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

        async Task<Expense?> IExpenseReadOnlyRepository.GetById(int id)
        {
            return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);

            if (result == null)
                return false;

            _dbContext.Expenses.Remove(result);
            return true;
        }

        async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(int id)
        {
            return await _dbContext.Expenses.Where(expense => expense.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }
    }
}
