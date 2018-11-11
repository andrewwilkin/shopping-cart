using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Infrastructure.Mappers;
using ShoppingCart.Api.Models.Dto.Carts;
using ShoppingCart.Api.Repositories.Implementation;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Tests
{
    [TestClass]
    public class CartRepositoryTests
    {
        private readonly ApiDbContext _dbContext;
        private readonly ICartRepository _cartRepository;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var mappings = new MapperConfigurationExpression();
            mappings.AddProfile<CartMaps>();
            mappings.AddProfile<ProductMaps>();
            Mapper.Initialize(mappings);
        }

        public CartRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            _dbContext = new ApiDbContext(options);
            TestData.Seed(_dbContext).Wait();

            var catalogRepository = new CatalogRepository(_dbContext);
            _cartRepository = new CartRepository(_dbContext, Mapper.Instance, catalogRepository);
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