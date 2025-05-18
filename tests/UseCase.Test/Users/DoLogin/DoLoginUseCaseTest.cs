using CashFlow.Application.UseCases.Users.DoLogin;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace UseCase.Test.Users.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestLoginUserJsonBuilder.Build();
            var user = UserBuilder.Build();
            request.Email = user.Email;
            var useCase = CreateUseCase(user, request.Password);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(user.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var request = RequestLoginUserJsonBuilder.Build();
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);
            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.LOGIN_INVALID));
        }

        [Fact]
        public async Task Error_Password_Not_Match()
        {
            var request = RequestLoginUserJsonBuilder.Build();
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);
            var result = await act.Should().ThrowAsync<InvalidLoginException>();

            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.LOGIN_INVALID));
        }

        private static IDoLoginUseCase CreateUseCase(CashFlow.Domain.Entities.User user, string password = null)
        {
            var _repository = new UserReadRepositoryBuilder().GetUserByEmail(user);
            IAccessTokenGenerator _accessToken = AccessTokenGeneratorBuilder.Build();

            var _passwordEncrypter = new PasswordEncrypterBuilder().Verify(password);

            return new DoLoginUseCase(
                _repository.Build(),
                _passwordEncrypter.Build(),
                _accessToken
            );
        }
    }
}
