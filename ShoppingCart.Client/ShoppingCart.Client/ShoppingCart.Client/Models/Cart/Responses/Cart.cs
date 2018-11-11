using System;
using System.Collections.Generic;

namespace ShoppingCart.Client.Models.Cart.Responses
{
    public class Cart
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> Products { get; set; } = new List<CartItem>();
    }
}
