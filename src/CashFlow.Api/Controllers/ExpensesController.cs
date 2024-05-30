using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Comunication.Requests;
using CashFlow.Comunication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
        {
            try
            {
                var useCase = new RegisterExpenseUseCase();
            
                var response = useCase.Execute(request);

                return Created(string.Empty, response);
            } catch (ArgumentException ex)
            {
                var errorResponse = new ResponseErrorJson(ex.Message);
                return BadRequest(errorResponse);
            } catch
            {
                var errorResponse = new ResponseErrorJson("Unkown error");
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
        
    }
}
