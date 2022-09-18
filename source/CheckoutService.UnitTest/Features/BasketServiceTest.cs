using CheckoutService.Features.Basket;
using CheckoutService.Features.Basket.Handler;
using CheckoutService.Persistence;
using Moq;
using Moq.EntityFrameworkCore;

namespace CheckoutService.UnitTest.Features
{
    public class BasketServiceTest
    {
        [Fact]
        public async Task GetBasket_Success()
        {
            var mockContext = new Mock<CheckoutDBContext>();
            var baskets = new List<Basket>();
            var basket = new Basket
            {
                BasketId = 1,
                CustomerName = "aa",
                PaysVat = false,
                Close = false,
                Payed = false,
                BasketProducts = new List<BasketProduct>
                {
                    new BasketProduct
                    {
                        ProductName = "apple",
                        ProductPrice = 100,
                        BasketId = 1
                    }
                }
            };

            baskets.Add(basket);

            mockContext.Setup(x => x.Baskets).ReturnsDbSet(baskets);

            //Test
            var service = new BasketService(mockContext.Object);

            var result = await service.GetBasketDetail(1, default);

            Assert.NotNull(result);
            Assert.Equal(result.Customer, basket.CustomerName);
        }
    }
}
