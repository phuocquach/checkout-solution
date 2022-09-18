using Checkout.Application.Basket;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get([FromQuery]int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateBasketRequest request)
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
