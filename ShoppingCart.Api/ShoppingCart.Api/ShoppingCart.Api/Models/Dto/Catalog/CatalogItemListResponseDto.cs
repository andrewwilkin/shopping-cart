using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Catalog
{
    public class CatalogItemListResponseDto : Resource
    {
        public List<CatalogItemResponseDto> CatalogItems { get; }

        public CatalogItemListResponseDto(IEnumerable<CatalogItemResponseDto> catalogItems)
        {
            CatalogItems = catalogItems.ToList();
        }
    }
}
