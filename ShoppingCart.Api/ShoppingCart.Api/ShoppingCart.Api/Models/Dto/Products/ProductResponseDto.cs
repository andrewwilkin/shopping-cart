using System;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Products
{
    public sealed class ProductResponseDto : Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
