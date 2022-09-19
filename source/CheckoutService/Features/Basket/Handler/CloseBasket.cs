using CheckoutService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Features.Basket.Handler
{
    public class CloseBasket
    {
        public class CloseBasketRequest : IRequest<Unit>
        {
            public int BasketId { get; set; }
            public bool Close { get; set; }
            public bool Payed { get; set; }
        }

        public class Handler : IRequestHandler<CloseBasketRequest, Unit>
        {
            private readonly CheckoutDBContext _dbContext;
            public Handler(CheckoutDBContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Unit> Handle(CloseBasketRequest request, CancellationToken cancellationToken)
            {

                if (request == null || request.BasketId <= 0)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var basket = await _dbContext.Baskets
                    .SingleOrDefaultAsync(x => x.BasketId == request.BasketId, cancellationToken);

                basket.Payed = request.Payed;
                basket.Close = request.Close;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }
        }

    }
}
