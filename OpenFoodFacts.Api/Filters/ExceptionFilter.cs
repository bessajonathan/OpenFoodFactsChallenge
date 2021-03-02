using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenFoodFacts.Common.Exceptions;
using System.Net;

namespace OpenFoodFacts.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            var response = new ExceptionResponse();

            response.Title = "Error";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Data = context.Exception.ToString();

            if (context.Exception is NotFoundException)
            {
                response.Title = "Warning";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Data = context.Exception.Message;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            if (context.Exception is ValidationException)
            {
                var errorValues = ((ValidationException)context.Exception).Failures.Values;

                var mensagem = "";

                foreach (var error in errorValues)
                    mensagem += "- " + error[0];

                response.Title = "Warning";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Data = $"Erro de validação: {mensagem}";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is IntegrationException)
            {
                response.Title = "Error";
                response.StatusCode = (int) HttpStatusCode.FailedDependency;
                response.Data = context.Exception.Message;
            }

            context.Result = new JsonResult(
                new
                {
                    response.Title,
                    response.StatusCode,
                    response.Data
                });
        }
    }
}
