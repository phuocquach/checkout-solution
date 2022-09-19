using CheckoutService.Features.Basket.Model;
using MediatR;

namespace CheckoutService.Features.Basket.Handler
{
    public class GetBasket
    {
        public class GetBasketRequest : IRequest<GetBasketResponse>
        {
            public int BasketId { get; set; }
        }

        public class Handler : IRequestHandler<GetBasketRequest, GetBasketResponse>
        {
            private readonly IBasketService _basketService;
            public Handler(IBasketService basketService)
            {
                _basketService = basketService;
            }
            public async Task<GetBasketResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
            {
                var basKetDetail = await _basketService.GetBasketDetail(request.BasketId, cancellationToken);
                if (basKetDetail == null)
                {
                    throw new Exception("Not found");
                }

                return new GetBasketResponse
                {
                    Id = basKetDetail.Id,
                    Customer = basKetDetail.Customer,
                    Items = basKetDetail.Items,
                    PaysVAT = basKetDetail.PaysVAT,
                    TotalGross = basKetDetail.TotalGross,
                    TotalNet = basKetDetail.TotalNet
                };

            }
        }
    }
}
