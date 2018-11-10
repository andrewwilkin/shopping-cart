using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Dto.Cart
{
    public class CartItemResponseDto
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
    }
}
