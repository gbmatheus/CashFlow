﻿using CashFlow.Comunication.Responses;
using CashFlow.Exception;
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
            var cashFlowException = (CashFlowException)context.Exception;
            var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());
            context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
            context.Result = new ObjectResult(errorResponse);

            //if (context.Exception is ErroOnValidationException erroOnValidationException)
            //{
            //    var errorResponse = new ResponseErrorJson(erroOnValidationException.Errors);
            //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    context.Result = new BadRequestObjectResult(errorResponse);
            //}
            //else if (context.Exception is NotFoundException notFoundException)
            //{
            //    var errorResponse = new ResponseErrorJson(notFoundException.Message);
            //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    context.Result = new NotFoundObjectResult(errorResponse);
            //}
            //else
            //{
            //    var errorResponse = new ResponseErrorJson(context.Exception.Message);
            //    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //    context.Result = new BadRequestObjectResult(errorResponse);
            //}
        }

        public void ThrowUnkownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
