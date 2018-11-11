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


        public async Task<Cart> RemoveShoppingCartItemAsync(Guid cartId, Guid itemId)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart == null)
                return null;

            var catalogItem = await _catalogRepository.FindByIdAsync(itemId);
            if (catalogItem == null)
                return null;

            var itemToRemove = cart.CartItems.FirstOrDefault(x => x.CatalogItemId == itemId);
            if (itemToRemove != null)
            {
                cart.CartItems.Remove(itemToRemove);
                await _dbContext.SaveChangesAsync();
            }

            return await EnrichCart(cart);
        }


        public async Task<Cart> IncreaseShoppingCartItemAsync(Guid cartId, Guid itemId, int quantity)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart == null)
                return null;

            var catalogItem = await _catalogRepository.FindByIdAsync(itemId);
            if (catalogItem == null)
                return null;

            if (cart.CartItems.Any(x => x.CatalogItemId == itemId))
            {
                var item = cart.CartItems.First(x => x.CatalogItemId == itemId);
                item.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    CartId = cartId,
                    CatalogItemId = itemId,
                    Quantity = quantity
                });
            }

            return await EnrichCart(cart);
        }


        public async Task<Cart> DecreaseShoppingCartItemAsync(Guid cartId, Guid itemId, int quantity)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart == null)
                return null;

            var catalogItem = await _catalogRepository.FindByIdAsync(itemId);
            if (catalogItem == null)
                return null;

            // Do not allow the quantity to go below zero
            // Arguably we should delete it but then makes it more difficult to increase from client
            if (cart.CartItems.Any(x => x.CatalogItemId == itemId))
            {
                var item = cart.CartItems.First(x => x.CatalogItemId == itemId);
                item.Quantity -= (quantity > item.Quantity) ? item.Quantity : quantity;               
            }
            
            // Do not throw error if the item not in the cart
            // Could equally throw a bad request but little benefit

            return await EnrichCart(cart);
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



    }
}
