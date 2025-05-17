using CashFlow.Comunication.Requests.Users;
using CashFlow.Comunication.Responses.Users;

namespace CashFlow.Application.UseCases.Users.DoLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseUserJson> Execute(RequestLoginUserJson request);
    }
}
