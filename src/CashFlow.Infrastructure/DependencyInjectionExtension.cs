using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Security.Tokens;
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

            services.AddScoped<IPasswordEncrypter, Security.Cryptography.BCrypt>();
            AddToken(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionServer = configuration.GetConnectionString("Connection");
            var version = new Version(9, 2, 0);
            var mySqlSeverVersion = new MySqlServerVersion(version);

            services.AddDbContext<CashFlowDBContext>(config => config.UseMySql(connectionServer, mySqlSeverVersion));
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            uint expirationTimeMinutes = configuration.GetValue<uint>("Setting:Jwt:ExpiresInMinutes");
            string signingKey = configuration.GetValue<string>("Setting:Jwt:SigningKey")!;
            services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpenseReadOnlyRepository, ExpenseRepository>();
            services.AddScoped<IExpenseWriteOnlyRepository, ExpenseRepository>();
            services.AddScoped<IExpenseUpdateOnlyRepository, ExpenseRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        }
    }
}
