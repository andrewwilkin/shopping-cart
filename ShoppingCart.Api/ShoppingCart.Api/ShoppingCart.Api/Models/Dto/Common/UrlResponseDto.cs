using Newtonsoft.Json;

namespace ShoppingCart.Api.Models.Dto.Common
{
    public class UrlResponseDto
    {
        public UrlResponseDto()
        {
        }

        public UrlResponseDto(string href)
        {
            Href = href;
        }

        [JsonProperty(Order = -99)]
        public string Href { get; }
    }
}
