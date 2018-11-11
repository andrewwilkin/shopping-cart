using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Carts;

namespace ShoppingCart.Api.Infrastructure.Mappers
{
    public class CartMaps : Profile
    {
        public CartMaps()
        {
            CreateMap<Cart, CartResponseDto>();
            CreateMap<CartItemRequestDto, CartItem>().ForMember(dest => dest.CatalogItemId, opt => opt.MapFrom(src => src.Id));
            CreateMap<CartItem, CartItemResponseDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CatalogItemId));
        }
    }
}
