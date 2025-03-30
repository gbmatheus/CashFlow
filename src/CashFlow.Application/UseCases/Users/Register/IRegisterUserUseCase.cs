using CashFlow.Comunication.Requests.Users;
using CashFlow.Comunication.Responses.Users;

namespace CashFlow.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
