using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Client.Helpers;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Services
{
    public class ProductServiceAsync : IProductServiceAsync
    {
        private readonly IApiHttpClient _httpClient;
        private readonly ApiEndpoints _endpoints;

        public ProductServiceAsync(IApiHttpClient httpClient,
            ApiConfig config)
        {
            _httpClient = httpClient;
            _endpoints = config.ApiEndpoints;
        }

        public async Task<(bool status,ProductList productList)> GetProductListAsync()
        {
            var response = await _httpClient.GetAsync<ProductList>(_endpoints.Products);
            if (!response.Success)
                return (false, null);

            return (true, response.Model);
        }

        public async Task<(bool status, Product product)> GetProductByIdAsync(Guid productId)
        {
            var response = await _httpClient.GetAsync<Product>(_endpoints.Product(productId));
            if (!response.Success)
                return (false, null);

            return (true, response.Model);
        }
    }
}
