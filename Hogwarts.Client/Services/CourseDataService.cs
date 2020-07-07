using Hogwarts.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hogwarts.Client.Services
{
    public class CourseDataService
    {
        private HttpClient _httpClient;

        public CourseDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            return await JsonSerializer.DeserializeAsync<List<CourseDto>>
               (await _httpClient.GetStreamAsync($"api/courses"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
