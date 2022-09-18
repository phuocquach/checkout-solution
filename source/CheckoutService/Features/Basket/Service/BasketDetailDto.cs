using Checkout.Application.Basket.Model;

namespace Checkout.Application.Service
{
    public class BasketDetailDto
    {
        public int Id { get; set; }
        public IEnumerable<BasketProductDto> Items { get; set; }
        public decimal TotalNet { get; set; }

        public decimal TotalGross { get; set; }

        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
    }
}
