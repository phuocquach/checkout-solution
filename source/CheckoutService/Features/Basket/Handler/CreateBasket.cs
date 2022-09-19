using CheckoutService.Persistence;
using FluentValidation;
using MediatR;

namespace CheckoutService.Features.Basket.Handler
{
    public class CreateBasket
    {
        public class CreateBasketRequest : IRequest<int>
        {
            public string Customer { get; set; }
            public bool PaysVAT { get; set; }
        }

        public class CreateBasketRequestValidator : AbstractValidator<CreateBasketRequest>
        {
            public CreateBasketRequestValidator()
            {
                RuleFor(request => request).NotNull();
                RuleFor(request => request.Customer).NotNull();
                RuleFor(request => request.Customer).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CreateBasketRequest, int>
        {
            private readonly CheckoutDBContext _dbContext;
            public Handler(CheckoutDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<int> Handle(CreateBasketRequest request, CancellationToken cancellationToken)
            {
                var basket = new Persistence.Basket
                {
                    CustomerName = request.Customer,
                    PaysVat = request.PaysVAT
                };

                await _dbContext.Baskets.AddAsync(basket, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return basket.BasketId;
            }
        }
    }
    
}
