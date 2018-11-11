using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Client.Models.Cart.Responses;

namespace ShoppingCart.Client.Tests
{
    [TestClass]
    public class CartServiceAsyncTests : BaseServiceTests
    {
        public CartServiceAsyncTests() : base()
        {           
        }

        [TestMethod]
        public async Task<Cart> CreateEmptyCartAsyncSuccess()
        {
            var (status, cart) = await ApiClientAsync.CartService.CreateCartAsync();
            Assert.IsTrue(status);
            Assert.IsNotNull(cart.Id);
            Assert.IsTrue(!cart.Products.Any());
            Assert.IsTrue(cart.Total == 0);
            return cart;
        }

        [TestMethod]
        public async Task AddProductToCartSuccess()
        {
            await ApiClientAsync.CartService.CreateCartAsync();
            var (_, productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            var (status, cart) = await ApiClientAsync.CartService.AddProductToCartAsync(productList.Products.First(), 5);
            Assert.IsTrue(cart.Products.Any());
            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task ReduceProductFromCartSuccess()
        {
            await ApiClientAsync.CartService.CreateCartAsync();
            var (_, productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            var product = productList.Products.First();
            await ApiClientAsync.CartService.AddProductToCartAsync(product, 5);
            var (status, cart) = await ApiClientAsync.CartService.ReduceProductFromCartAsync(product, 2);
            Assert.IsTrue(cart.Products.First().Quantity == 3);
            Assert.IsTrue(status);
            var (_, emptyCart) = await ApiClientAsync.CartService.ReduceProductFromCartAsync(product, 10);
            Assert.IsTrue(emptyCart.Products.First().Quantity == 0);
        }

        [TestMethod]
        public async Task RemoveProductFromCartSuccess()
        {
            await ApiClientAsync.CartService.CreateCartAsync();
            var (_, productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            var product = productList.Products.First();
            await ApiClientAsync.CartService.AddProductToCartAsync(product, 5);
            var (status, cart) = await ApiClientAsync.CartService.RemoveProductFromCartAsync(product);
            Assert.IsTrue(status);
            Assert.IsTrue(!cart.Products.Any());
        }


        [TestMethod]
        public async Task ResetCartAsyncSuccess()
        {
            await ApiClientAsync.CartService.CreateCartAsync();
            var (_, productList) = await ApiClientAsync.ProductService.GetProductListAsync();
            var (_, populatedCart) = await ApiClientAsync.CartService.AddProductToCartAsync(productList.Products.First(), 5);
            Assert.IsTrue(populatedCart.Products.Any());
            var (status, cart) = await ApiClientAsync.CartService.ResetCartAsync();
            Assert.IsTrue(status);
            Assert.IsTrue(!cart.Products.Any());
        }


        [TestMethod]
        public async Task GetCartAsyncSuccess()
        {
            var testCart = await CreateEmptyCartAsyncSuccess();
            var (_, cart) = await ApiClientAsync.CartService.GetCartAsync();
            Assert.IsNotNull(cart.Id);
            Assert.AreEqual(testCart.Id, cart.Id);
        }


        [TestMethod]
        public async Task DeleteCartAsyncSuccess()
        {
            var cart = await CreateEmptyCartAsyncSuccess();
            var result = await ApiClientAsync.CartService.DestroyCartAsync();
            Assert.IsTrue(result);
            var response = await ApiClientAsync.CartService.GetCartAsync();
            Assert.IsFalse(response.status);
            Assert.IsNull(response.cart);
        }

        [TestMethod]
        public async Task DeleteCartAsyncFail()
        {
            var result = await ApiClientAsync.CartService.DestroyCartAsync();
            Assert.IsFalse(result);
        }
    }
}
