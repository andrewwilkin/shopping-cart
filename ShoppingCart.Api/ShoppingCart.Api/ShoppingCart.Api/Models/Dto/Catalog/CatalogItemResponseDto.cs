using System;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Catalog
{
    public sealed class CatalogItemResponseDto : Resource
    {
        public Guid Id { get; }
        public string Name { get; }

        public CatalogItemResponseDto(Guid id, 
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}
