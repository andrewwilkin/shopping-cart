using System.Net;
using Newtonsoft.Json;

namespace ShoppingCart.Api.Models.Dto.Common
{
    public abstract class Resource
    {
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = -99)]
        //public StatusResponseDto Status { get; set; } = new StatusResponseDto();
    }
}
