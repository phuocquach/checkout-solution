using CheckoutService.Persistence;
using MediatR;

namespace CheckoutService.Features.Basket.Handler
{
    public class CreateBasketRequest : IRequest<int>
    {
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
    }

    public class CreateBasketHandler : IRequestHandler<CreateBasketRequest, int>
    {
        private readonly CheckoutDBContext _dbContext;
        public CreateBasketHandler(CheckoutDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateBasketRequest request, CancellationToken cancellationToken)
        {
            if (request == null || request.Customer == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

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
