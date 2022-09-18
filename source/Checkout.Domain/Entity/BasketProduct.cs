using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout.Domain.Entity
{
    [Table("BasketProduct")]
    public class BasketProduct
    {
        [Key]
        public int BasketProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public int BasketId { get; set; }
        [ForeignKey("BasketId")]
        public Basket Basket { get; set; }
    }
}