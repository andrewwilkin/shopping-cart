using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingCart.Api.Models.Dto;
using ShoppingCart.Api.Models.Dto.Common;

namespace ShoppingCart.Api.Infrastructure.Filters
{
    public class SelfReferenceFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executed = await next();

            var url = context.HttpContext.Request.GetDisplayUrl();

            var result = executed?.Result as ObjectResult;

            if (result?.Value is Resource resource)
            {
                resource.Self = new UrlResponseDto(url);
            }
        }
    }
}
