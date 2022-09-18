using CheckoutService.Features.Basket.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CheckoutService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseController
    {
        public BasketsController(IMediator mediator) : base(mediator) 
        {
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetBasket.Request
            {
                BasketId = id
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateBasket.CreateBasketRequest request)
        {
            var result = await Mediator.Send(request);

            return Created("", new { Id = result });
        }

        [HttpPut("{id}/article-line")]
        public async Task<ActionResult> Put([FromRoute]int id, [FromBody] AddBasketProductRequest request)
        {
            request.BasketId = id;

            var result = await Mediator.Send(request);

            return Ok(result);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] CloseBasket.Request request)
        {
            request.BasketId = id;

            var result = await Mediator.Send(request);

            return Ok(result);
        }
    }
}
