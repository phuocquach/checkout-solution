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

            return Ok(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }


        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] string value)
        {

        }
    }
}
