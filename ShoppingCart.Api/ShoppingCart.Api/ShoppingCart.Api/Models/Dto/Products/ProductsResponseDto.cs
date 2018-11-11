using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Models.Dto.Products
{
    public class ProductsResponseDto : Resource
    {
        public List<ProductResponseDto> Products { get; }

        public ProductsResponseDto(IEnumerable<ProductResponseDto> products)
        {
            Products = products.ToList();
        }
    }
}
