using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Client.Helpers;
using ShoppingCart.Client.Models.Products;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Tests
{
    [TestClass]
    public class ApiHttpClientTests
    {
        private readonly ApiConfig _config;
        private readonly ApiEndpoints _endpoints;
        private readonly ApiHttpClient _httpClient;

        public ApiHttpClientTests()
        {
            _config = new ApiConfig();
            _endpoints = new ApiEndpoints(_config);
            _httpClient = new ApiHttpClient(_config);
        }

        [TestMethod]
        public async Task GetProductsSuccess()
        {
            var response = await _httpClient.GetAsync<ProductList>(_endpoints.Products);
            Assert.AreEqual(response.HttpStatusCode, HttpStatusCode.OK);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Model);
        }

        [TestMethod]
        public async Task GetProductsFail()
        {
            var response = await _httpClient.GetAsync<ProductList>("");
            Assert.IsFalse(response.Success);
            Assert.IsTrue(response.ErrorMessage != string.Empty);
        }
    }
}
