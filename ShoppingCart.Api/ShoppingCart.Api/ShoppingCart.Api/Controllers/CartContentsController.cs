using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto.Carts;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Controllers
{
    /// <summary>
    /// Add some more user friendly ways of manipulating the contents of a cart
    /// </summary>
    
    [Route("api/carts")]
    [Produces("application/json")]
    [ApiController]
    public class CartContentsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartContentsController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Removes a product from the cart
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request:
        ///
        ///     DELETE /api/cart/{cartId}/{itemId}
        /// 
        /// </remarks>
        /// <returns>Cart</returns>
        /// <response code="200">Returns the updated cart</response>
        /// <response code="404">Cart or catalog item not found</response>
        [ProducesResponseType(200, Type = typeof(CartResponseDto))]
        [ProducesResponseType(404)]
        [HttpDelete("{cartId:guid}/products/{productId:guid}")]
        public async Task<IActionResult> RemoveShoppingCartItemAsync(Guid cartId,
            [FromRoute] Guid productId)
        {
            var cart = await _cartRepository.RemoveShoppingCartItemAsync(cartId, productId);
            if (cart == null)
                return NotFound();

            var result = _mapper.Map<CartResponseDto>(cart);
            return Ok(result);
        }

        /// <summary>
        /// Increases the quantity of a product in the cart
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request (defaults quantity to 1):
        ///
        ///     POST /api/cart/{cartId}/items/{itemId}/add
        ///
        /// Sample request with quantity:
        ///
        ///     POST /api/cart/{cartId}/items/{itemId}/add/{quantity}
        /// 
        /// </remarks>
        /// <returns>Cart</returns>
        /// <response code="200">Returns the updated cart</response>
        /// <response code="404">Cart or catalog item not found</response>
        [ProducesResponseType(200, Type = typeof(CartResponseDto))]
        [ProducesResponseType(404)]
        [HttpPost("{cartId:guid}/products/{productId:guid}/add/{quantity:int?}")]
        public async Task<IActionResult> IncreaseShoppingCartItemAsync(Guid cartId,
            [FromRoute] Guid productId,
            [FromRoute] int quantity = 1)
        {
            var cart = await _cartRepository.IncreaseShoppingCartItemAsync(cartId, productId, quantity);

            if (cart == null)
                return NotFound();

            var result = _mapper.Map<CartResponseDto>(cart);
            return Ok(result);
        }
        /// <summary>
        /// Decreases the quantity of a product in the cart
        /// </summary>
        /// <remarks>
        /// 
        /// Sample request (defaults quantity to 1):
        ///
        ///     POST /api/cart/{cartId}/items/{itemId}/remove
        ///
        /// Sample request with quantity:
        ///
        ///     POST /api/cart/{cartId}/items/{itemId}/remove/{quantity}
        ///
        /// Note:   Will not fail if product not in the cart or quantity less than zero,
        ///         Rather will set to sensible values
        /// 
        /// </remarks>
        /// <returns>Cart</returns>
        /// <response code="200">Returns the updated cart</response>
        /// <response code="404">Cart or catalog item not found</response>
        [ProducesResponseType(200, Type = typeof(CartResponseDto))]
        [ProducesResponseType(404)]
        [HttpPost("{cartId:guid}/products/{productId:guid}/remove/{quantity:int?}")]
        public async Task<IActionResult> DecreaseShoppingCartItemAsync(Guid cartId,
            [FromRoute] Guid productId,
            [FromRoute] int quantity = 1)
        {
            var cart = await _cartRepository.DecreaseShoppingCartItemAsync(cartId, productId, quantity);

            if (cart == null)
                return NotFound();

            var result = _mapper.Map<CartResponseDto>(cart);
            return Ok(result);
        }

    }
}
