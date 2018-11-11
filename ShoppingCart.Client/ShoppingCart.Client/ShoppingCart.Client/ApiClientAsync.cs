using System;
using ShoppingCart.Client.Helpers;
using ShoppingCart.Client.Services;

namespace ShoppingCart.Client
{
    public class ApiClientAsync : IApiClientAsync
    {
        private readonly ApiConfig _config;
        private readonly IApiHttpClient _apiHttpClient;
        public readonly ICartServiceAsync CartService;
        public readonly IProductServiceAsync ProductService;

        public ApiClientAsync(ApiConfig config)
        {
            _config = config;
            _apiHttpClient = new ApiHttpClient(_config);
            CartService = new CartServiceAsync(_apiHttpClient, _config);
            ProductService = new ProductServiceAsync(_apiHttpClient, _config);
        }

    }
}
