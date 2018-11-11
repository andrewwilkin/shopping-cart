using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Client.Models.Common;

namespace ShoppingCart.Client.Helpers
{
    public interface IApiHttpClient
    {
        Task<HttpResponse<T>> GetAsync<T>(string resource);
        Task<HttpResponse<T>> PostAsync<T>(string resource, object payload);
        Task<HttpResponse<T>> DeleteAsync<T>(string resource);
    }
}
