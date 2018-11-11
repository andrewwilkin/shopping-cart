using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Client.Models.Products.Responses;

namespace ShoppingCart.Client.Tests
{
    [TestClass]
    public class ProductServiceAsyncTests : BaseServiceTests
    {
        public ProductServiceAsyncTests() : base()
        {           
        }

        [TestMethod]
        public async Task GetProductsListAsync()
        {
            (var status, ProductList productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            Assert.IsTrue(status);
            Assert.IsTrue(productList.Products.Any());
        }

        [TestMethod]
        public async Task GetProductByIdAsyncSuccess()
        {
            (var status, ProductList productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            Assert.IsTrue(status);
            Assert.IsTrue(productList.Products.Any());
            var testProduct = productList.Products.First();
            (var newStatus, Product fetchedProduct) = await ApiClientAsync.ProductService.GetProductByIdAsync(testProduct.Id);
            Assert.IsTrue(newStatus);
            Assert.AreEqual(testProduct.Name, fetchedProduct.Name);
            Assert.AreEqual(testProduct.UnitPrice, fetchedProduct.UnitPrice);
        }

        [TestMethod]
        public async Task GetProductByIdAsyncFail()
        {
            (var status, Product product) = await ApiClientAsync.ProductService.GetProductByIdAsync(Guid.NewGuid());
            Assert.IsFalse(status);
            Assert.IsNull(product);
        }
    }
}
