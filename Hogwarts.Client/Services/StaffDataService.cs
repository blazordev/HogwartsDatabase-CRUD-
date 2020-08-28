using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class StaffDataService
    {
        private readonly HttpClient _httpClient;
        public StaffDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<StaffDto>>
                (await _httpClient.GetStreamAsync($"api/staff"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<StaffDto> GetStaffByIdAsync(int staffId)
        {
            return await JsonSerializer.DeserializeAsync<StaffDto>
                (await _httpClient.GetStreamAsync($"api/staff/{staffId}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<StaffDto> AddStaff(StaffDto staff)
        {
            var staffJson =
                new StringContent(JsonSerializer.Serialize(staff), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/staff", staffJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<StaffDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<StaffDto> UpdateStaff(StaffDto staff)
        {
            var staffJson =
                new StringContent(JsonSerializer.Serialize(staff), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/staff/{staff.Id}", staffJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<StaffDto>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task DeleteStaffCollection(string staffIds)
        {
            await _httpClient.DeleteAsync($"api/staffCollections/({staffIds})");
        }

        public async Task<string> UpdateStaffCollection(IEnumerable<StaffDto> staffCollection)
        {
            var staffCollectionJson =
                new StringContent(JsonSerializer.Serialize(staffCollection), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/staffCollections", staffCollectionJson);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<byte[]> Download()
        {
            var response = await _httpClient.GetAsync("api/files/staff");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }

}

