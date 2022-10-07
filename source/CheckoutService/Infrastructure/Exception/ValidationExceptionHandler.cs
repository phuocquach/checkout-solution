using CheckoutService.Common;
using CheckoutService.Infrastructure.AppInterceptor;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace CheckoutService.Infrastructure.Exception
{
    public class ValidationExceptionHandler
    {
        public static async Task Handle(ValidationException validationException, HttpContext httpContext)
        {
            var apiErrorResponse = new ApiErrorResponse
            {
                HttpCode = (int)HttpStatusCode.BadRequest,
                Details = validationException.ErrorDetails
            };

            var jsonError = JsonConvert.SerializeObject(apiErrorResponse
               , new JsonSerializerSettings
               {
                   ContractResolver = new CamelCasePropertyNamesContractResolver()
               });

            httpContext.Response.StatusCode = apiErrorResponse.HttpCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(jsonError);
        }
    }
}
