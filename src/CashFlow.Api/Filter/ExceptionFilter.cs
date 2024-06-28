﻿using CashFlow.Comunication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CashFlowException)
                HandlerProjectException(context);
            else
                ThrowUnkownError(context);
        }

        public void HandlerProjectException(ExceptionContext context)
        {
            if(context.Exception is ErroOnValidationException)
            {
                var ex = (ErroOnValidationException)context.Exception;
                var errorResponse = new ResponseErrorJson(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
            else
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        public void ThrowUnkownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson("Unkown error");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
