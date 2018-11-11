using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Client.Helpers
{
    public class ApiEndpoints
    {
        private readonly ApiConfig _config;

        public ApiEndpoints(ApiConfig config)
        {
            _config = config;
        }

        private string GetUrl(string endpointName)
        {
            return string.Concat(_config.BaseApiUrl, endpointName);
        }

        public string Carts => GetUrl("/carts");
        public string Cart (Guid cartId) => GetUrl($"/carts/{cartId}");
        public string CartProduct(Guid cartId, Guid productId) => GetUrl($"/carts/{cartId}/products/{productId}");
        public string AddProductToCart(Guid cartId, Guid productId, int quantity) => GetUrl($"/carts/{cartId}/products/{productId}/add/{quantity}");
        public string ReduceProductInCart(Guid cartId, Guid productId, int quantity) => GetUrl($"/carts/{cartId}/products/{productId}/remove/{quantity}");
        public string Products => GetUrl("/products");
        public string Product (Guid productId) => GetUrl($"/products/{productId}");
    }
}
