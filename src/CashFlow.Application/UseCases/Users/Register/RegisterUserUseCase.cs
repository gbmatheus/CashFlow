using AutoMapper;
using CashFlow.Comunication.Requests.Users;
using CashFlow.Comunication.Responses.Users;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IMapper mapper,
            IPasswordEncrypter passwordEncrypter,
            IUnitOfWork unitOfWork
        )
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _mapper = mapper;
            _passwordEncrypter = passwordEncrypter;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = _mapper.Map<User>(request);
            user.Password = _passwordEncrypter.Encrypter(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseUserJson
            {
                Name= user.Name,
                Token = ""
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);
            if (emailExist)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errosMensages = result.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErroOnValidationException(errosMensages);
            }
        }
    }
}
