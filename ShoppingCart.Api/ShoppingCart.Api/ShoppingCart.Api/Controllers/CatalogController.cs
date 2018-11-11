using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Api.Models.Data;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Catalog;
using ShoppingCart.Api.Models.Interfaces;
using ShoppingCart.Api.Repositories.Interfaces;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/catalog")]
    [Produces("application/json")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CatalogController(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
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
        [ProducesResponseType(200, Type = typeof(CatalogItemListResponseDto))]
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var items = await _catalogRepository.FetchAllAsync();
            var dto = new CatalogItemListResponseDto(_mapper.Map<List<CatalogItemResponseDto>>(items));
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
        [ProducesResponseType(200, Type = typeof(CatalogItemResponseDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var item = await _catalogRepository.FindByIdAsync(id);

            if (item == null)
                return NotFound();
            
            var dto = new CatalogItemResponseDto(item.Id, item.Name);
            return Ok(dto);
        }
    }
}
