using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Products;
using ShoppingCart.Api.Models.Interfaces;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICatalogRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(ICatalogRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets entire catalog
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/catalog/
        ///
        /// </remarks>
        /// <returns>Catalog List From The Repository</returns>
        /// <response code="200">Returns the Catalog List from the repository</response>
        [ProducesResponseType(200, Type = typeof(ProductsResponseDto))]
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var items = await _productsRepository.FetchAllAsync();
            var dto = new ProductsResponseDto(_mapper.Map<List<ProductResponseDto>>(items));
            return Ok(dto);
        }

        /// <summary>
        /// Gets a specific Catalog Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/catalog/{guid}
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Catalog Item From The Repository</returns>
        /// <response code="200">Returns the Catalog Item from the repository</response>
        /// <response code="404">If the catalog item cannot be found</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(ProductResponseDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var item = await _productsRepository.FindByIdAsync(id);

            if (item == null)
                return NotFound();

            var dto = _mapper.Map<List<ProductResponseDto>>(item);
            return Ok(dto);
        }
    }
}
