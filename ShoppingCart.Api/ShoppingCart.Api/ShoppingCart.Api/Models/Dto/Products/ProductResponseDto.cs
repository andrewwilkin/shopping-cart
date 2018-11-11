using System;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Products
{
    public sealed class ProductResponseDto : Resource
    {
        public Guid Id { get; }
        public string Name { get; }

        public ProductResponseDto(Guid id, 
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}
