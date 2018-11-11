using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ShoppingCart.Client.Models.Common
{
    public class HttpResponse<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public T Model { get; set; }

        public HttpResponse(T model)
        {
            Model = model;
        }
    }
}
