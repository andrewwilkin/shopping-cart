﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Api.Models.Dto.Cart
{
    public class CartContentsRequestDto
    {
        public List<CartItemRequestDto> Products { get; set; } = new List<CartItemRequestDto>();
    }
}
