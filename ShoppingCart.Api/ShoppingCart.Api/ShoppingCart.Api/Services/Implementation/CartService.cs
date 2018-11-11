using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Api.Repositories.Interfaces;
using ShoppingCart.Api.Services.Interfaces;

namespace ShoppingCart.Api.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


    }
}
