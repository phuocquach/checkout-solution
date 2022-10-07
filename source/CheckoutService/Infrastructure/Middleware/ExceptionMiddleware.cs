using CheckoutService.Infrastructure.AppInterceptor;
using CheckoutService.Infrastructure.Exception;
using System.Globalization;
using System.Net;


namespace CheckoutService.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
               
                await ValidationExceptionHandler.Handle(ex, httpContext);
            }

            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context?.Response?.WriteAsync(new ErrorDetail
            {
                Code = context?.Response?.StatusCode.ToString(CultureInfo.InvariantCulture),
                Message = exception.ToString()
            }.ToString());
        }
    }
}
