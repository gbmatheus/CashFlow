using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.DoLogin;
using CashFlow.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddUseCase(services);
            AddAutoMapper(services);
        }

        public static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
            services.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
            services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();

            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<DoLoginUseCase, DoLoginUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }
    }
}
