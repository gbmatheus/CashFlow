using CashFlow.Comunication.Requests.Users;
using CashFlow.Comunication.Responses.Users;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly IAccessTokenGenerator _accessToken;

        public DoLoginUseCase(IUserReadOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessToken)
        {
            _repository = repository;
            _passwordEncrypter = passwordEncrypter;
            _accessToken = accessToken;
        }

        public async Task<ResponseUserJson> Execute(RequestLoginUserJson request)
        {
            User? user = await _repository.GetUserByEmail(request.Email);

            if (user is null)
                throw new InvalidLoginException(ResourceErrorMessages.LOGIN_INVALID);

            bool passwordValid = _passwordEncrypter.Verify(request.Password, user.Password);

            if (!passwordValid)
                throw new InvalidLoginException(ResourceErrorMessages.LOGIN_INVALID);

            string token = _accessToken.Generate(user);

            return new ResponseUserJson
            {
                Name = user!.Name,
                Token = token
            };
        }
    }
}
