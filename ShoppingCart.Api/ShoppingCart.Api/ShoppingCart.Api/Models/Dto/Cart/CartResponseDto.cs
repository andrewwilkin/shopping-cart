using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Cart
{
    public class CartResponseDto : Resource
    {
        public Guid Id { get; set; }

        public decimal TotalPrice => CartItems.Sum(x => x.LinePrice);

        public List<CartItemResponseDto> CartItems { get; set; } = new List<CartItemResponseDto>();
    }
}
