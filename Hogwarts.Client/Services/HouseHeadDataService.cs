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
                var contentStream = await response.Content.ReadAsStreamAsync();

                try
                {
                    return await JsonSerializer.DeserializeAsync<HouseDto>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                }
                catch (JsonException) // Invalid JSON
                {
                    Console.WriteLine("Invalid JSON.");
                }
            }
            else
            {
                Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
            }

            return null;
        }

    }
}

