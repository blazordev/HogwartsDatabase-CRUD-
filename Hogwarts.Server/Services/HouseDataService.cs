using Hogwarts.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Server.Services
{
    public class HouseDataService
    {
        private HttpClient _httpClient;

        public HouseDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<HouseDto>> GetAllHousesAsync()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<HouseDto>>
               (await _httpClient.GetStreamAsync($"api/houses"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
