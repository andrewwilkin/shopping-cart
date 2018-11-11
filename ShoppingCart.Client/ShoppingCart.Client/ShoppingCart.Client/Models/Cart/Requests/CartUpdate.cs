using System.Collections.Generic;

namespace ShoppingCart.Client.Models.Cart.Requests
{
    public class CartUpdate
    {
        public List<CartItem> Products = new List<CartItem>();
    }
}
