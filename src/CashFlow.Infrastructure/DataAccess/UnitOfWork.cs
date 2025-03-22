using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private CashFlowDBContext _dbContext;

        public UnitOfWork(CashFlowDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
