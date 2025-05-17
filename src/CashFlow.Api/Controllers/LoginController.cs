using CashFlow.Application.UseCases.Users.DoLogin;
using CashFlow.Comunication.Requests.Users;
using CashFlow.Comunication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestLoginUserJson request
        )
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
