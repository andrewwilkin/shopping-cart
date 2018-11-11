using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Catalog;
using ShoppingCart.Api.Models.Interfaces;

namespace ShoppingCart.Api.Infrastructure.Mappers
{
    public class CatalogMaps : Profile
    {
        public CatalogMaps()
        {
            CreateMap<CatalogItem, CatalogItemResponseDto>();
        }
    }
}
