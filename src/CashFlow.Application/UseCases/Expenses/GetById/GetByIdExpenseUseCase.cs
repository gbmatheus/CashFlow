using AutoMapper;
using CashFlow.Comunication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetByIdExpenseUseCase : IGetByIdExpenseUseCase
    {
        private readonly IExpenseReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdExpenseUseCase(IExpenseReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseExpenseJson> Execute(int id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUNT);

            return _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
