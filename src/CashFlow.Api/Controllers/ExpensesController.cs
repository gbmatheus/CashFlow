﻿using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Comunication.Requests.Expenses;
using CashFlow.Comunication.Responses;
using CashFlow.Comunication.Responses.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterExpenseUseCase useCase,
            [FromBody] RequestExpenseJson request
        )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllExpenseUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Expenses.Count != 0)
                return Ok(response);

            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(
            [FromServices] IGetByIdExpenseUseCase useCase,
            [FromRoute] int id
        )
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
           [FromServices] IDeleteExpenseUseCase useCase,
           [FromRoute] int id
        )
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
           [FromServices] IUpdateExpenseUseCase useCase,
           [FromRoute] int id,
           [FromBody] RequestExpenseJson request
        )
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
    }
}
