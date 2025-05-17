using AutoMapper;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Security.Cryptography;
using System.Security.Cryptography;
using CommonTestUtilities.Request;
using FluentAssertions;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Cryptography;

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

        private static RegisterUserUseCase CreateUseCase()
        {
            IMapper mapper = MapperBuilder.Builder();
            IUnitOfWork unitOfWork = UnitOfWorkBuilder.Build();
            IPasswordEncrypter passwordEncrypter = PasswordEncrypterBuilder.Build();

            var userReadOnlyRepository = new UserReadRepositoryBuilder();
            IUserWriteOnlyRepository userWriteOnlyRepository = UserWriteRepositoryBuilder.Build();
            
            return new RegisterUserUseCase(userReadOnlyRepository.Build(), userWriteOnlyRepository, mapper, passwordEncrypter, unitOfWork);
        }
    }
}
