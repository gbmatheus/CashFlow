using AutoMapper;
using CashFlow.Comunication.Requests.Expenses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IExpenseUpdateOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateExpenseUseCase(
            IExpenseUpdateOnlyRepository repository, 
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(int id, RequestExpenseJson request)
        {
            Validate(request);
            var expense = await _repository.GetById(id);

            if (expense is null)
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUNT);

            _mapper.Map(request, expense);
            _repository.Update(expense);
            await _unitOfWork.Commit();
        }

        private void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errosMensages = result.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErroOnValidationException(errosMensages);
            }
        }
    }
}
