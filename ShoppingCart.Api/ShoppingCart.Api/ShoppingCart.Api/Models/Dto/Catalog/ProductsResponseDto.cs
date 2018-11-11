using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Catalog
{
    public class ProductsResponseDto : Resource
    {
        public List<ProductResponseDto> CatalogItems { get; }

        public ProductsResponseDto(IEnumerable<ProductResponseDto> catalogItems)
        {
            CatalogItems = catalogItems.ToList();
        }
    }
}
