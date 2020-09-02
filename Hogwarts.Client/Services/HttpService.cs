using Hogwarts.Client.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Hogwarts.Client.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions defaultJsonSerializerOptions =>
           new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await _httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var responseString = await responseHTTP.Content.ReadAsStringAsync();
                var response = System.Text.Json.JsonSerializer
                     .Deserialize<T>(responseString, defaultJsonSerializerOptions);

                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, responseHTTP);
            }
        }
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var dataJson = System.Text.Json.JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            var dataJson = System.Text.Json.JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, responseHTTP.IsSuccessStatusCode, responseHTTP);
        }
    }
}
