using CheckoutService.Infrastructure.AppInterceptor;

namespace CheckoutService.Common
{
    public class ApiErrorResponse
    {
        public int HttpCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public List<ErrorDetail> Details { get; set; }
    }

}

