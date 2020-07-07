using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class HouseDataService
    {
        private HttpClient _httpClient;

        public HouseDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HouseDto>> GetAllHousesAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<HouseDto>>
               (await _httpClient.GetStreamAsync($"api/houses"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
