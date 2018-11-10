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

        public CartRepository(ApiDbContext dbContext, 
            IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            return cart;
        }

        public async Task<Cart> UpdateShoppingCartAsync(Guid cartId, 
            CartContentsRequestDto cartContentsRequest)
        {
            var cart = await FindByIdAsync(cartId);
            if (cart == null)
                return null;

            cart.CartItems = _mapper.Map<List<CartItem>>(cartContentsRequest.CartContents).ToList();
            _dbContext.Update(cart);

            await _dbContext.SaveChangesAsync();
            return cart;
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


        public override async Task<Cart> FindByIdAsync(Guid id) => await _dbContext
               .Carts
               .Include(e => e.CartItems)
               .FirstOrDefaultAsync(x => x.Id == id);       
    }
}
