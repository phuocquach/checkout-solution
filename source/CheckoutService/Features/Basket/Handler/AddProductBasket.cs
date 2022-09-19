using CheckoutService.Features.Basket.Model;
using CheckoutService.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Features.Basket.Handler
{
    public class AddProductBasket
    {
        public class AddProductBasketRequest : IRequest<BasketProductDto>
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

        public class Handler : IRequestHandler<AddProductBasketRequest, BasketProductDto>
        {
            private readonly CheckoutDBContext _dbContext;
            public Handler(CheckoutDBContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<BasketProductDto> Handle(AddProductBasketRequest request, CancellationToken cancellationToken)
            {
                var basket = await _dbContext.Baskets
                    .SingleOrDefaultAsync(x => x.BasketId == request.BasketId, cancellationToken);

                if (basket == null || basket.Close || basket.Payed)
                {
                    throw new InvalidOperationException("Can not alter this basket");
                }

                var basketProduct = new BasketProduct
                {
                    ProductName = request.Item,
                    ProductPrice = request.Price,
                    Basket = basket
                };

                await _dbContext.BasketProducts.AddAsync(basketProduct);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new BasketProductDto
                {
                    Id = basketProduct.BasketProductId,
                    Item = basketProduct.ProductName,
                    Price = basketProduct.ProductPrice
                };
            }
        }
    }
    
}
