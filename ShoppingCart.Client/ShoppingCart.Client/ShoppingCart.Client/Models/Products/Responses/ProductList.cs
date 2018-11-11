using System.Collections.Generic;

namespace ShoppingCart.Client.Models.Products.Responses
{
    public class ProductList
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
