using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout.Domain.Entity
{
    [Table("BasketProduct")]
    public class BasketProduct
    {
        public int BasketProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        [ForeignKey("Basket")]
        public int BasketId;

        public Basket Basket;
    }
}