using System;

namespace ShoppingCart.Client.Models.Cart.Requests
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
