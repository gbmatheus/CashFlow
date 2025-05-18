using AutoMapper;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Request;
using CommonTestUtilities.Tokens;
using FluentAssertions;

namespace UseCase.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var useCase = CreateUseCase();
            var request = RequestRegisterUserJsonBuilder.Builder();

            var result = await useCase.Execute(request);
            
            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var useCase = CreateUseCase();
            var request = RequestRegisterUserJsonBuilder.Builder();
            request.Name = string.Empty;

            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroOnValidationException>();
            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_EMPTY));
        }

        [Fact]
        public async Task Error_Email_Already_Exists()
        {
            var request = RequestRegisterUserJsonBuilder.Builder();
            var useCase = CreateUseCase(request.Email);
            
            var act = async () => await useCase.Execute(request);

            var result = await act.Should().ThrowAsync<ErroOnValidationException>();
            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        private static RegisterUserUseCase CreateUseCase(string email = null)
        {
            IMapper mapper = MapperBuilder.Builder();
            IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
            IPasswordEncrypter passwordEncrypter = PasswordEncrypterBuilder.Build();
            IAccessTokenGenerator accessToken = AccessTokenGeneratorBuilder.Build();

            var userReadOnlyRepository = new UserReadRepositoryBuilder();
            if(!string.IsNullOrWhiteSpace(email))
                userReadOnlyRepository.ExistsActiveUserWithEmail(email);

            IUserWriteOnlyRepository userWriteOnlyRepository = UserWriteRepositoryBuilder.Build();
            
            return new RegisterUserUseCase(userReadOnlyRepository.Build(), userWriteOnlyRepository, mapper, passwordEncrypter, unitOfWork, accessToken);
        }
    }
}
