﻿using Checkout.ApplicationException;
using Checkout.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Checkout.Application.Basket
{
    public class AddBasketProductRequest: IRequest<Unit>
    {
        public int BasketId { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }

    }
    public class AddProductBasketHandler: IRequestHandler<AddBasketProductRequest>
    {
        private readonly CheckoutDBContext _dbContext;
        public AddProductBasketHandler(CheckoutDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddBasketProductRequest request, CancellationToken cancellationToken)
        {
            if (request == null || request.BasketId <= 0)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var basket = await _dbContext.Baskets
                .SingleOrDefaultAsync(x => x.BasketId == request.BasketId, cancellationToken);

            if (basket.Close)
            {
                throw new BadRequestException("This basket is closed");
            }

            if (basket.Payed)
            {
                throw new BadRequestException("This basket is payed");
            }

            basket.BasketProducts.Add(new Domain.Entity.BasketProduct
            {
                ProductName = request.Item,
                ProductPrice = request.Price
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
