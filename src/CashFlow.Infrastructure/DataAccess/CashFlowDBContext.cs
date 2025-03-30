using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class CashFlowDBContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users{ get; set; }

        public CashFlowDBContext(DbContextOptions options) : base(options) { }
    }
}
