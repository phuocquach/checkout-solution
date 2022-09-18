using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout.Domain.Entity
{
    [Table("Basket")]
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }
        public string CustomerName { get; set; }
        public bool PaysVat { get; set; }

        [InverseProperty("Basket")]
        public List<BasketProduct> BasketProducts { get; set; } = new List<BasketProduct>();

        public bool Close { get; set; }

        public bool Payed { get; set; }

    }
}