using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Contexts;
using ShoppingCart.Api.Models.Dto.Cart;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Repositories.Implementation
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICatalogRepository _catalogRepository;

        public CartRepository(ApiDbContext dbContext, 
            IMapper mapper, 
            ICatalogRepository catalogRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _catalogRepository = catalogRepository;
        }

        public async Task<Cart> CreateShoppingCartAsync(CartContentsRequestDto cartContentsRequest)
        {
            var cart = new Cart();

            if (cartContentsRequest.CartContents.Any())
            {
                cart.CartItems = _mapper.Map<List<CartItem>>(cartContentsRequest.CartContents).ToList();
            }
            _dbContext.Add(cart);

            await _dbContext.SaveChangesAsync();
            return await EnrichCart(cart);
            //return cart;
        }

        public async Task<Cart> UpdateShoppingCartAsync(Guid cartId, 
            CartContentsRequestDto cartContentsRequest)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart == null)
                return null;

            cart.CartItems = _mapper.Map<List<CartItem>>(cartContentsRequest.CartContents).ToList();
            cart.UpdatedAt = DateTimeOffset.UtcNow;

            _dbContext.Update(cart);

            await _dbContext.SaveChangesAsync();
            return await EnrichCart(cart);
            //return cart;
        }


        public async Task RemoveShoppingCartAsync(Guid cartId)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart != null)
            {
                _dbContext.Carts.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Look to find a better way - perhaps wrap the cart model with the extended data?
        private async Task<Cart> EnrichCart(Cart cart)
        {
            if (cart == null)
                return null;

            foreach (var item in cart.CartItems)
            {
                var catalogItem = await _catalogRepository.FindByIdAsync(item.CatalogItemId);
                item.UnitPrice = catalogItem.UnitPrice;
                item.Name = item.Quantity > 1 ? catalogItem.NamePlural : catalogItem.Name;
            }

            return cart;
        }


        public override async Task<Cart> FindByIdAsync(Guid id)
        {
            var cart = await _dbContext
                .Carts
                .Include(e => e.CartItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            return await EnrichCart(cart);
            //return cart;
        }
    }
}
