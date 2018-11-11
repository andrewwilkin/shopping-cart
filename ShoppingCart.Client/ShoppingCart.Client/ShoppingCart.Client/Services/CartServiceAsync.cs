using System;
using System.Threading.Tasks;
using ShoppingCart.Client.Helpers;
using ShoppingCart.Client.Models.Cart.Requests;
using ShoppingCart.Client.Models.Cart.Responses;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Services
{
    public class CartServiceAsync : ICartServiceAsync
    {
        private readonly IApiHttpClient _httpClient;
        private readonly ApiEndpoints _endpoints;
        private Guid _cartId;

        public CartServiceAsync(IApiHttpClient httpClient, 
            ApiConfig config)
        {
            _httpClient = httpClient;
            _endpoints = config.ApiEndpoints;
        }

        public async Task<(bool status, Cart cart)> GetCartAsync()
        {
            var response = await _httpClient.GetAsync<Cart>(_endpoints.Cart(_cartId));
            if (!response.Success)
                return (false, null);

            return (true, response.Model);
        }


        public async Task<(bool status, Cart cart)> CreateCartAsync()
        {
            var response = await _httpClient.PostAsync<Cart>(_endpoints.Carts, new object());
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<(bool status, Cart cart)> ResetCartAsync()
        {
            var response = await _httpClient.PostAsync<Cart>(_endpoints.Cart(_cartId), new object());
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<(bool status, Cart cart)> ReplaceCartContentsAsync(CartUpdate cartUpdateRequest)
        {
            var response = await _httpClient.PostAsync<Cart>(_endpoints.Cart(_cartId), cartUpdateRequest);
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<(bool status, Cart cart)> AddProductToCartAsync(Product product, int quantity)
        {
            var response = await _httpClient.PostAsync<Cart>(_endpoints.AddProductToCart(_cartId, product.Id, quantity),new object());
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<(bool status, Cart cart)> ReduceProductFromCartAsync(Product product, int quantity)
        {
            var response = await _httpClient.PostAsync<Cart>(_endpoints.ReduceProductInCart(_cartId, product.Id, quantity), new object());
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<(bool status, Cart cart)> RemoveProductFromCartAsync(Product product)
        {
            var response = await _httpClient.DeleteAsync<Cart>(_endpoints.CartProduct(_cartId, product.Id));
            if (!response.Success)
                return (false, null);

            _cartId = response.Model.Id;
            return (true, response.Model);
        }

        public async Task<bool> DestroyCartAsync()
        {
            var response = await _httpClient.DeleteAsync<object>(_endpoints.Cart(_cartId));
            return response.Success;
        }
    }
}
