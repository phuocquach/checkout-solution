﻿using Checkout.Application.Service;
using CheckoutService.Features.Basket.Handler;
using CheckoutService.Persistence;
using Moq;
using Moq.EntityFrameworkCore;

namespace CheckoutService.UnitTest.Features
{
    public class BasketHandlerTest
    {
        [Fact]
        public async Task HandleGetBasket_Success()
        {
            var mockContext = new Mock<IBasketService>();
            var baskets = new List<Basket>();

            baskets.Add(new Basket
            {
                CustomerName = "aa",
                PaysVat = false
            });

            mockContext.Setup(x => x.GetBasketDetail(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new BasketDetailDto
            {

            });

            //Test
            var handler = new GetBasket.Handler(mockContext.Object);

            var result = await handler.Handle(new GetBasket.Request
            {
                BasketId = 1,
            }, default);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task HandleCreateBasket_Success()
        {
            var mockContext = new Mock<CheckoutDBContext>();
            var baskets = new List<Basket>();

            baskets.Add(new Basket
            {
                CustomerName = "aa",
                PaysVat = false
            });

            mockContext.Setup(x => x.Baskets).ReturnsDbSet(baskets);

            //Test
            var handler = new CreateBasket.Handler(mockContext.Object);

            var result = await handler.Handle(new CreateBasket.CreateBasketRequest
            {
                Customer = Guid.NewGuid().ToString(),
                PaysVAT = true
            }, default);
        }
    }
}
