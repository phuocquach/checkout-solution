using Checkout.Domain.Common;
using CheckoutService.Features.Basket.Model;
using CheckoutService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService.Features.Basket
{
    public interface IBasketService
    {
        Task<BasketDetailDto> GetBasketDetail(int basketId, CancellationToken cancellationToken);
    }

    public class BasketService : IBasketService
    {
        private readonly CheckoutDBContext _dbContext;
        public BasketService(CheckoutDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BasketDetailDto> GetBasketDetail(int basketId, CancellationToken cancellationToken)
        {
            
            var basket = await _dbContext.Baskets.Include(x => x.BasketProducts).SingleOrDefaultAsync(x => x.BasketId == basketId, cancellationToken: cancellationToken);
            if (basket == null) return null;

            var items = basket.BasketProducts.Select(x => new BasketProductDto
            {
                Item = x.ProductName,
                Price = x.ProductPrice
            });
            var totalNet = items.Sum(i => i.Price);
            var totalGross = totalNet + (totalNet * DefaultValue.VAT);

            var result = new BasketDetailDto
            {
                Customer = basket.CustomerName,
                Id = basket.BasketId,
                PaysVAT = basket.PaysVat,
                Items = items,
                TotalGross = totalGross,
                TotalNet = totalNet
            };
            return result;
        }
    }
}
