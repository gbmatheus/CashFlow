﻿using AutoMapper;
using CashFlow.Comunication.Responses;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpenseUseCase : IGetAllExpenseUseCase
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        public GetAllExpenseUseCase(IExpenseRepository repository, IMapper mapper)
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
