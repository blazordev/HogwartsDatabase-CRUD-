using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class RolesDataService
    {
        private HttpClient _httpClient;

        public RolesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<RoleDto>>
               (await _httpClient.GetStreamAsync($"api/roles"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
