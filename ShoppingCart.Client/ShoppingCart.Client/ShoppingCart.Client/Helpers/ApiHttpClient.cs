using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCart.Client.Models.Common;

namespace ShoppingCart.Client.Helpers
{
    public class ApiHttpClient : IApiHttpClient
    {
        private readonly ApiConfig _config;

        public ApiHttpClient(ApiConfig config)
        {
            _config = config;
        }

        public async Task<HttpResponse<T>> GetAsync<T>(string resource)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                SetStandardHeaders(httpClient);

                HttpResponseMessage httpResp = null;
                try
                {
                    var task = httpClient.GetAsync(resource);
                    httpResp = await task;
                }
                catch (Exception ex)
                {
                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp?.StatusCode ?? HttpStatusCode.BadRequest,
                        ErrorMessage = ex.Message
                    };
                }

                if (httpResp.IsSuccessStatusCode)
                {
                    string respJson = await httpResp.Content.ReadAsStringAsync();
                    if (respJson.Length > 0)
                        return new HttpResponse<T>(JsonConvert.DeserializeObject<T>(respJson))
                        {
                            HttpStatusCode = httpResp.StatusCode,
                            Success = true
                        };
                }
                else
                {
                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp.StatusCode,
                        ErrorMessage = httpResp.ReasonPhrase
                    };
                }
                return null;
            }
        }


        public async Task<HttpResponse<T>> PostAsync<T>(string resource, object payload)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                SetStandardHeaders(httpClient);

                HttpResponseMessage httpResp = null;
                try
                {
                    var task = httpClient.PostAsync(resource, SerializePayload(payload));
                    httpResp = await task;
                }
                catch (Exception ex)
                {
                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp?.StatusCode ?? HttpStatusCode.BadRequest,
                        ErrorMessage = ex.Message
                    };
                }

                if (httpResp.IsSuccessStatusCode)
                {
                    string respJson = await httpResp.Content.ReadAsStringAsync();
                    if (respJson.Length > 0)
                        return new HttpResponse<T>(JsonConvert.DeserializeObject<T>(respJson))
                        {
                            HttpStatusCode = httpResp.StatusCode,
                            Success = true
                        };
                }
                else
                {
                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp.StatusCode,
                        ErrorMessage = httpResp.ReasonPhrase
                    };
                }
                return null;
            }
        }


        public async Task<HttpResponse<T>> DeleteAsync<T>(string resource)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                SetStandardHeaders(httpClient);

                HttpResponseMessage httpResp = null;
                try
                {
                    var task = httpClient.DeleteAsync(resource);
                    httpResp = await task;
                }
                catch (Exception ex)
                {
                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp?.StatusCode ?? HttpStatusCode.BadRequest,
                        ErrorMessage = ex.Message
                    };
                }

                if (httpResp.IsSuccessStatusCode)
                {
                    string respJson = await httpResp.Content.ReadAsStringAsync();
                    if (respJson.Length > 0)
                        return new HttpResponse<T>(JsonConvert.DeserializeObject<T>(respJson))
                        {
                            HttpStatusCode = httpResp.StatusCode,
                            Success = true
                        };

                    return new HttpResponse<T>(default(T))
                    {
                        HttpStatusCode = httpResp.StatusCode,
                        Success = true
                    };
                }

                return new HttpResponse<T>(default(T))
                {
                    HttpStatusCode = httpResp.StatusCode,
                    ErrorMessage = httpResp.ReasonPhrase
                };
            }
        }



        private StringContent SerializePayload(object payload)
        {
            string strReq = JsonConvert.SerializeObject(payload, Formatting.Indented);
            return new StringContent(strReq, Encoding.UTF8, "application/json");
        }


        private void SetStandardHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
