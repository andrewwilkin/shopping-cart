using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Client.Helpers
{
    public class ApiConfig
    {
        private const string DevUrl = "https://localhost:44383/api";

        public string BaseApiUrl { get; private set; } = DevUrl;
        public ApiEndpoints ApiEndpoints { get; private set; }

        public ApiConfig()
        {
            ApiEndpoints = new ApiEndpoints(this);
        }
    }
}
