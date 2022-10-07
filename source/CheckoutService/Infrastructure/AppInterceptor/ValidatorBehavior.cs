using FluentValidation;
using MediatR;

namespace CheckoutService.Infrastructure.AppInterceptor
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var commandName = request.GetType().FullName;

            var requestValidator = _serviceProvider.GetService<IValidator<TRequest>>();

            if (requestValidator != null)
            {
                var validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var fatalSeverity = (Severity)3;
                    var fatalError = validationResult.Errors.FirstOrDefault(x => x.Severity == fatalSeverity);
                   
                    throw new BadHttpRequestException("Invalid request");
                }
            }

            var response = await next();

            return response;
        }

    }
}
