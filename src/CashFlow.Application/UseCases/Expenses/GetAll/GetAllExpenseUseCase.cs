﻿using AutoMapper;
using CashFlow.Comunication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpenseUseCase : IGetAllExpenseUseCase
    {
        private readonly IExpenseReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllExpenseUseCase(IExpenseReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseExpensesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseExpensesJson 
            { 
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }
    }
}
