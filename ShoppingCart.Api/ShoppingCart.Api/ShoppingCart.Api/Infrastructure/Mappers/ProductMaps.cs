using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Products;
using ShoppingCart.Api.Models.Interfaces;

namespace ShoppingCart.Api.Infrastructure.Mappers
{
    public class ProductMaps : Profile
    {
        public ProductMaps()
        {
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
