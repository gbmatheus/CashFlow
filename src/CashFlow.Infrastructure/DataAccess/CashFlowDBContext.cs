using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    public class CashFlowDBContext: DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionServer = "server=127.0.0.1;uid=root;pwd=Senha123;database=cashflow_db";
            var version = new Version(9, 2, 0);
            var mySqlSeverVersion = new MySqlServerVersion(version);

            optionsBuilder.UseMySql(connectionServer, mySqlSeverVersion);
        }
    }
}
