using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using OpenFoodFacts.Common.Configurations;
using OpenFoodFacts.Common.Exceptions;
using System.Net;

namespace OpenFoodFacts.API.Filters
{
    public class AuthorizationFilter : IActionFilter
    {
        private readonly string ApiKey;
        public AuthorizationFilter(IOptions<ApiSettings> settings)
        {
            if (string.IsNullOrEmpty(settings.Value.ApiKey))
                throw new NotFoundException("Apikey não definida.");

            ApiKey = settings.Value.ApiKey;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var response = new ExceptionResponse();

            var headerKey = context.HttpContext.Request.Headers.TryGetValue("API_KEY", out var extractedApiKey);

            if (!headerKey)
            {
                response.Title = "Unauthorized";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Data = "Chave da Api não informada no header.";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(
                    new
                    {
                        response.Title,
                        response.StatusCode,
                        response.Data
                    });

                return;
            }

            if (!ApiKey.Equals(extractedApiKey))
            {
                response.Title = "Unauthorized";
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Data = "Chave da Api inválida.";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                context.Result = new JsonResult(
                    new
                    {
                        response.Title,
                        response.StatusCode,
                        response.Data
                    });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
