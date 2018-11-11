using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Client.Helpers;

namespace ShoppingCart.Client.Tests
{
    public class BaseServiceTests
    {
        protected ApiClientAsync ApiClientAsync;

        public BaseServiceTests()
        {
            var config = new ApiConfig();
            ApiClientAsync = new ApiClientAsync(config);
        }
    }
}
