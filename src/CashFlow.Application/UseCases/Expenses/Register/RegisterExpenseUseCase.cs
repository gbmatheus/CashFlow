using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase: IRegisterExpenseUseCase
    {
        private readonly IExpenseRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterExpenseUseCase(IExpenseRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            var entity = new Expense
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Amount = request.Amount,
                PaymentType = (PaymentType)request.PaymentType
            };

            _repository.Add(entity);
            _unitOfWork.Commit();

            return new ResponseRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if(!result.IsValid)
            {
                var errosMensages = result.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErroOnValidationException(errosMensages);
            }
        }

    }
}
