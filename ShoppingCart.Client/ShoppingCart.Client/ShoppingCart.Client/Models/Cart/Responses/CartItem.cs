using System;

namespace ShoppingCart.Client.Models.Cart.Responses
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
