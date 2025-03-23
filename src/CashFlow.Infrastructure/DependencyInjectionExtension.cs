using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionServer = configuration.GetConnectionString("Connection");
            var version = new Version(9, 2, 0);
            var mySqlSeverVersion = new MySqlServerVersion(version);

            services.AddDbContext<CashFlowDBContext>(config => config.UseMySql(connectionServer, mySqlSeverVersion));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpenseReadOnlyRepository, ExpenseRepository>();
            services.AddScoped<IExpenseWriteOnlyRepository, ExpenseRepository>();
        }
    }
}
