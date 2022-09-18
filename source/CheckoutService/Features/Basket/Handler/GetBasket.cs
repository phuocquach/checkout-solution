using Checkout.Application.Basket.Model;
using Checkout.Application.Service;
using MediatR;

namespace CheckoutService.Features.Basket.Handler
{
    public class GetBasket
    {
        public class Request : IRequest<GetBasketResponse>
        {
            public int BasketId { get; set; }
        }

        public class Handler : IRequestHandler<Request, GetBasketResponse>
        {
            private readonly IBasketService _basketService;
            public Handler(IBasketService basketService)
            {
                _basketService = basketService;
            }
            public async Task<GetBasketResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                var basKetDetail = await _basketService.GetBasketDetail(request.BasketId, cancellationToken);
                if (basKetDetail == null)
                {
                    throw new Exception("Not found exception");
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
