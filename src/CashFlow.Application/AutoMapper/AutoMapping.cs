using AutoMapper;
using CashFlow.Comunication.Requests.Expenses;
using CashFlow.Comunication.Responses.Expenses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestExpenseJson, Expense>();
        }

        private void EntityToResponse()
        {
            CreateMap<Expense, ResponseRegisteredExpenseJson>();
            CreateMap<Expense, ResponseShortExpenseJson>();
            CreateMap<Expense, ResponseExpenseJson>();
        }


    }
}
