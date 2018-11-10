using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Models.Dto.Cart;
using ShoppingCart.Api.Repositories.Implementation;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Tests
{
    [TestClass]
    public class CartRepositoryTests
    {
        private readonly ApiDbContext _dbContext;
        private readonly ICartRepository _cartRepository;
        

        public CartRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            _dbContext = new ApiDbContext(options);
            TestData.Seed(_dbContext).Wait();

            //_cartRepository = new CartRepository(_dbContext);
        }

        [TestMethod]
        public async Task CreateNewCartTest()
        {
            var cart = await _cartRepository.CreateShoppingCartAsync(new CartContentsRequestDto());
            var result = await _dbContext.Carts.AnyAsync(x => x.Id == cart.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task FindCartByIdTest()
        {
            var cart = await _cartRepository.CreateShoppingCartAsync(new CartContentsRequestDto());
            var fetchedCart = await _cartRepository.FindByIdAsync(cart.Id);
            Assert.IsNotNull(fetchedCart);

        }
    }
}
