﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout.Domain.Entity
{
    [Table("Basket")]
    public class Basket
    {
        public int BasketId { get; set; }
        public string CustomerName { get; set; }
        public bool PaysVat { get; set; }

        [InverseProperty("Basket")]
        public IEnumerable<BasketProduct> BasketProducts;
    }
}