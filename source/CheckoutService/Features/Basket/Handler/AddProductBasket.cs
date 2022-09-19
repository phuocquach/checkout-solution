using CheckoutService.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Features.Basket.Handler
{
    public class AddProductBasket
    {
        public class AddProductBasketRequest : IRequest<Unit>
        {
            public int BasketId { get; set; }
            public string Item { get; set; }
            public decimal Price { get; set; }

        }

        public class AddProductBasketRequestValidator : AbstractValidator<AddProductBasketRequest>
        {
            public AddProductBasketRequestValidator()
            {
                RuleFor(request => request.Price).GreaterThanOrEqualTo(0);
                RuleFor(request => request.Item).NotNull();
                RuleFor(request => request.Item).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddProductBasketRequest>
        {
            private readonly CheckoutDBContext _dbContext;
            public Handler(CheckoutDBContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Unit> Handle(AddProductBasketRequest request, CancellationToken cancellationToken)
            {
                var basket = await _dbContext.Baskets
                    .SingleOrDefaultAsync(x => x.BasketId == request.BasketId, cancellationToken);

                if (basket == null)
                {
                    throw new Exception("This basket is not existed");
                }

                if (basket.Close)
                {
                    throw new Exception("This basket is closed");
                }

                if (basket.Payed)
                {
                    throw new Exception("This basket is payed");
                }

                basket.BasketProducts.Add(new BasketProduct
                {
                    ProductName = request.Item,
                    ProductPrice = request.Price
                });

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
    
}
