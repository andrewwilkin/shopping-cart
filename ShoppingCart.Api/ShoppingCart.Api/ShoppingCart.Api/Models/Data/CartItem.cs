using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Data
{
    public class CartItem
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid CatalogItemId { get; set; }
        public CatalogItem CatalogItem { get; set; }

        public decimal Quantity { get; set; }

        [NotMapped]
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public string Name { get; set; }
    }
}
