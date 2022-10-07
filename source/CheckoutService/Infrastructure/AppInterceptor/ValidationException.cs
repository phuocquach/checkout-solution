namespace CheckoutService.Infrastructure.AppInterceptor
{
    public class ValidationException: System.Exception
    {
        public List<ErrorDetail> ErrorDetails { get; set; }
        public int ErrorCode { get; set; }
    }

    public class ErrorDetail
    {
        public string Message { get; set; }

        public string Code { get; set; }
    }
}
