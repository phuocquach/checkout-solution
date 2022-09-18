namespace Checkout.Application.Basket.Model
{
    public class GetBasketResponse
    {
        public int Id { get; set; }
        public IEnumerable<BasketProductDto> Items { get; set; }
        public decimal TotalNet { get; set; }

        public decimal TotalGross { get; set; }

        public string Customer { get; set; }
        public bool PaysVAT { get; set; }

    }

    public class BasketProductDto
    {
        public string Item { get; set; }
        public decimal Price { get; set; }
    }
}
