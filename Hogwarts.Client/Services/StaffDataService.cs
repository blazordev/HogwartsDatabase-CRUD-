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
        private JsonSerializerOptions jsonSerializerOprions =
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public StaffDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            var response = await _httpClient.GetStreamAsync($"api/staff");
            return await JsonSerializer.DeserializeAsync<List<StaffDto>>
                (response, jsonSerializerOprions);
        }

        public async Task<StaffDto> GetStaffByIdAsync(int staffId)
        {
            var response = await _httpClient.GetStreamAsync($"api/staff/{staffId}");
            return await JsonSerializer.DeserializeAsync<StaffDto>
                (response, jsonSerializerOprions);
        }

        public async Task<string> AddStaff(StaffDto staff)
        {
            var staffJson = JsonSerializer.Serialize(staff);
            var stringContent = new StringContent(staffJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/staff", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
            return await response.Content.ReadAsStringAsync();


        }

        public async Task<StaffDto> UpdateStaff(StaffDto staff)
        {
            var staffJson = JsonSerializer.Serialize(staff);
            var stringContent = new StringContent(staffJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/staff/{staff.Id}", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<StaffDto>(await response.Content.ReadAsStreamAsync());
            }

            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> DeleteStaffCollection(string staffIds)
        {
           var response = await _httpClient.DeleteAsync($"api/staffCollections/({staffIds})");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> UpdateStaffCollection(IEnumerable<StaffDto> staffCollection)
        {
            var staffCollectionJson =
                new StringContent(JsonSerializer.Serialize(staffCollection), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/staffCollections", staffCollectionJson);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> Download()
        {
            var response = await _httpClient.GetAsync("api/files/staff");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }

}

