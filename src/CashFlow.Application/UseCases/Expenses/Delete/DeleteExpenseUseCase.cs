using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpenseWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenseReadOnlyRepository _repositoryRead;

        public DeleteExpenseUseCase(IExpenseWriteOnlyRepository repository, IUnitOfWork unitOfWork,
            IExpenseReadOnlyRepository repositoryRead)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _repositoryRead = repositoryRead;
        }

        public async Task Execute(int id)
        {
            var resultTeste = await _repositoryRead.GetById(id);
            var result = await _repository.Delete(id);

            if (result == false)
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUNT);

            await _unitOfWork.Commit();
        }
    }
}
