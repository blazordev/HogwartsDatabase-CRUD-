using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class HouseHeadDataService
    {
        private HttpClient _httpClient;

        public HouseHeadDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HouseDto> GetHouseForStaffAsync(int staffId)
        {           
            var response = await _httpClient.GetAsync($"api/HeadOfHouseAssignments/Staff/{staffId}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<HouseDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;

        }
    }
}

