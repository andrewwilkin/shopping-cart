using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Dto.Cart
{
    public class CartItemRequestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
